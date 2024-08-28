using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;
using Humanizer;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Infrastructure.Scoping;

namespace FamilyHubs.OR.Umbraco;

/// <summary>
/// Generates Umbraco content and content types.
/// </summary>
public interface IUmbracoContentGenerator
{
    /// <summary>
    /// Generates an umbraco document type based on a given generic type.
    /// </summary>
    /// <typeparam name="TModel">The type used to create the document type.</typeparam>
    /// <param name="creatingUserId">The ID of the user the creation is assigned too (Guid.Empty does not work!)</param>
    /// <param name="options"></param>
    Task GenerateUmbracoDocumentType<TModel>(Guid creatingUserId, UmbracoContentGenerator.GeneratorOptions options);
}

public class UmbracoContentGenerator(
    IContentTypeService contentTypeService,
    IContentService contentService,
    IUmbracoDataTypeLoader umbracoDataTypeLoader,
    IShortStringHelper shortStringHelper,
    ILogger<UmbracoContentGenerator> logger,
    IScopeProvider scopeProvider
) : IUmbracoContentGenerator
{
    public class GeneratorOptions
    {
        public bool DropIfExists { get; init; }
        public string ItemIcon { get; init; } = "";
        public string ParentItemIcon { get; init; } = "";
        public GenerateParentContentItemOption? GenerateParentContentItem { get; init; }

        public enum GenerateParentContentItemOption
        {
            CreateIfNotExists,
            DropIfExistsAndCreate
        }
    }
    
    public async Task GenerateUmbracoDocumentType<TModel>(Guid creatingUserId, GeneratorOptions options)
    {
        string name = typeof(TModel).Name;
        string alias = shortStringHelper.CleanString(name, CleanStringType.CamelCase);

        IContentType parentContentType = await GenerateUmbracoDocumentTypeParent(name, creatingUserId, options);

        IContentType? existingContentType = contentTypeService.Get(name);
        if (existingContentType is not null)
        {
            if (options.DropIfExists)
            {
                logger.LogDebug("Content type '{Name}' already exists; dropping it...", name);
                await contentTypeService.DeleteAsync(existingContentType.GetUdi().Guid, creatingUserId);
                logger.LogInformation("Dropped document type '{Name}'", name);
            }
            else
            {
                logger.LogWarning("Content type '{Name}' already exists; skipping creation.", name);
                return;
            }
        }
        
        logger.LogDebug("Generating and creating content type '{Name}'...", name);

        ContentType contentType = new(shortStringHelper, -1)
        {
            Name = name,
            Alias = alias,
            Description = "Default description",
            Icon = options.ItemIcon
        };

        PropertyGroup group = new(new PropertyTypeCollection(false))
        {
            Name = "Properties",
            Alias = "properties",
        };

        IEnumerable<PropertyType> generatedProperties = typeof(TModel).GetProperties()
            .Select(async prop => await BuildUmbracoPropertyTypeFromPropertyInfo(prop))
            .Select(t => t.Result)
            .ToList();
            
        group.PropertyTypes = new PropertyTypeCollection(false, generatedProperties);

        contentType.PropertyGroups.Add(group);

        // Create the content type
        await contentTypeService.CreateAsync(contentType, creatingUserId);
        logger.LogInformation("Generated and created document type '{Name}'", name);

        // Allow the parent content type to list this content type underneath it
        await AllowContentTypeOnParentContentType(parentContentType, contentType);
    }

    private async Task<IContentType> GenerateUmbracoDocumentTypeParent(
        string childModelName,
        Guid creatingUserId,
        GeneratorOptions options)
    {
        string name = PluraliseOpenReferralContentType(childModelName);

        IContentType? existingDocumentType = contentTypeService.Get(name);
        if (existingDocumentType is not null)
        {
            if (options.DropIfExists)
            {
                logger.LogDebug("Parent document type '{Name}' for document type '{ChildName}' already exists; dropping it...", name, childModelName);
                await contentTypeService.DeleteAsync(existingDocumentType.GetUdi().Guid, creatingUserId);
                logger.LogInformation("Dropped parent document type '{Name}'", name);
            }
            else
            {
                logger.LogWarning("Parent document type '{Name}' for document type '{ChildName}' already exists; skipping creation.", name, childModelName);
                return existingDocumentType;
            }
        }

        logger.LogDebug("Generating and creating parent document type '{Name}' for document type '{ChildName}'...", name, childModelName);
    
        string alias = ToAlias(name);
        
        ContentType contentType = new(shortStringHelper, -1)
        {
            Name = name,
            Alias = alias,
            Description = "Default description",
            Icon = options.ParentItemIcon,
            AllowedAsRoot = true
        };

        await contentTypeService.CreateAsync(contentType, creatingUserId);
        logger.LogInformation("Generated and created parent document type '{Name}' for document type '{ChildName}'", name, childModelName);

        if (options.GenerateParentContentItem is not null)
            GenerateParentContentItem(options, name, existingDocumentType, alias);
        
        return contentType;
    }

    private void GenerateParentContentItem(
        GeneratorOptions options,
        string name,
        IContentType? existingDocumentType,
        string alias)
    {
        IContent? existingParentNode = contentService.GetRootContent().FirstOrDefault(node => node.Name == name);
        if (existingParentNode is not null)
        {
            switch (options.GenerateParentContentItem)
            {
                case GeneratorOptions.GenerateParentContentItemOption.CreateIfNotExists:
                    logger.LogWarning("Skipping creation of parent content item: item {Name} already exists in the document tree.", name);
                    break;
                    
                case GeneratorOptions.GenerateParentContentItemOption.DropIfExistsAndCreate:
                    contentService.Delete(existingParentNode);
                    logger.LogInformation("Deleted existing parent node for '{Name}' (Guid = {Guid})", name, existingDocumentType!.AsGuid());
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException($"Option '{options.GenerateParentContentItem}' is not supported.");
            }
        }

        IContent parentNode = contentService.CreateAndSave(name, -1, alias);
        contentService.Publish(parentNode, ["*"]);
            
        logger.LogInformation("Created new parent node for '{Name}'", name);
    }

    private async Task AllowContentTypeOnParentContentType(
        IContentType parentContentType,
        IContentType allowedContentType)
    {
        using IScope scope = scopeProvider.CreateScope();
        
        // TODO: The below is a hack to work around an apparent bug in Umbraco when allowing content types
        //
        // The proper (broken) way:
        // parentContentType.AllowedContentTypes =
        // [
        //     new ContentTypeSort(Guid.NewGuid(), 1, allowedContentType.Alias)
        // ];
        //
        // await contentTypeService.UpdateAsync(parentContentType, creatingUserId);
        
        await scope.Database.ExecuteAsync(
            $"""
             INSERT INTO [dbo].[cmsContentTypeAllowedContentType] (Id, AllowedId, SortOrder)
             VALUES (@0, @1, @2)
             """,
            parentContentType.Id,
            allowedContentType.Id,
            0);

        scope.Complete();
        
        logger.LogInformation("Updated {ParentName} ({ParentId}, {ParentAlias}) content type to allow children of type {ChildName} ({ChildId}, {ChildAlias})", 
            parentContentType.Name,
            parentContentType.Id,
            parentContentType.Alias,
            allowedContentType.Name,
            allowedContentType.Id,
            allowedContentType.Alias);
    }
    
    private async Task<PropertyType> BuildUmbracoPropertyTypeFromPropertyInfo(PropertyInfo propertyInfo)
    {
        string? jsonPropertyName = propertyInfo.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name;
        if (jsonPropertyName is null)
        {
            logger.LogWarning(
                "Could not create content type property for {PropertyName}: No JSON property name found.",
                propertyInfo.Name
            );

            return null!;
        }

        PropertyType umbracoProperty = await MapTypeToUmbracoPropertyType(propertyInfo);

        // Use JSON property name to maintain property names between the API and internally in Umbraco
        umbracoProperty.Name = jsonPropertyName;
        // Umbraco aliases _should_ be camel case
        umbracoProperty.Alias = shortStringHelper.CleanString(propertyInfo.Name, CleanStringType.CamelCase);
        // Default to mandatory if the property is a non-nullable reference type
        umbracoProperty.Mandatory = propertyInfo.GetCustomAttribute<RequiredAttribute>() is not null;
        
        if (string.IsNullOrWhiteSpace(umbracoProperty.Description))
            umbracoProperty.Description = "Default description.";

        return umbracoProperty;
    }

    private async Task<PropertyType> MapTypeToUmbracoPropertyType(PropertyInfo propertyInfo)
    {
        Type propertyType = ResolvePropertyType(propertyInfo);
        
        PropertyType umbracoProperty;
        // Map scalar types to Umbraco property types
        if (propertyType == typeof(string))
            umbracoProperty = new(shortStringHelper, await umbracoDataTypeLoader.Textarea());
        else if (propertyType == typeof(int))
            umbracoProperty = new(shortStringHelper, await umbracoDataTypeLoader.Numeric());
        else if (propertyType == typeof(DateTime))
            umbracoProperty = new (shortStringHelper, await umbracoDataTypeLoader.DatePickerWithTime());
        // Map enumerable types to Umbraco content picker property types (e.g. List<Location>)
        else if (propertyType.IsEnumerable() && propertyType.IsGenericType)
        {
            // Get type of the enumerable
            Type genericType = propertyType.GetGenericArguments()[0];

            if (!IsOpenReferralType(genericType))
            {
                logger.LogWarning(
                    "Could not determine best content type property for {PropertyName}: Property type {PropertyTypeName} is not a supported Open Referral type.",
                    propertyInfo.Name,
                    genericType
                );

                umbracoProperty = await BuildDefaultUmbracoPropertyType(propertyInfo);
            }
            else
            {
                IContentType? umbracoContentType = contentTypeService.GetAll()
                    .FirstOrDefault(ct => 
                        ct.Alias.Equals(genericType.Name, StringComparison.CurrentCultureIgnoreCase));
                if (umbracoContentType is null)
                {
                    logger.LogWarning(
                        "Could not determine best content type property for {PropertyName}: No content type found for model '{GenericTypeName}'.",
                        propertyInfo.Name,
                        genericType.Name
                    );

                    umbracoProperty = await BuildDefaultUmbracoPropertyType(propertyInfo);
                }
                else
                {
                    IDataType contentPickerDataType = await umbracoDataTypeLoader.ContentPicker();
                    contentPickerDataType.ConfigurationData = new Dictionary<string, object>()
                    {
                        { "minNumber", 0 },
                        { "maxNumber", 0 }, // Unlimited
                        { "filter", umbracoContentType.Alias }
                    };

                    umbracoProperty = new(shortStringHelper, contentPickerDataType);
                }
            }
        }
        // Map non-enumerable Open Referral types (e.g. Location)
        else if (IsOpenReferralType(propertyInfo.PropertyType))
        {
            IContentType? umbracoContentType = contentTypeService.GetAll()
                .FirstOrDefault(ct => ct.Alias == propertyType.Name);
            if (umbracoContentType is null)
            {
                logger.LogWarning(
                    "Could not determine best content type property for {PropertyName}: No content type found for model '{PropertyTypeName}'.",
                    propertyInfo.Name,
                    propertyType.Name
                );

                umbracoProperty = await BuildDefaultUmbracoPropertyType(propertyInfo);
            }
            else
            {
                umbracoProperty = new(shortStringHelper, await umbracoDataTypeLoader.DocumentPicker())
                {
                    DataTypeId = umbracoContentType.Id,
                };
            }
        }
        else
        {
            // Default to Textarea for now.
            umbracoProperty = await BuildDefaultUmbracoPropertyType(propertyInfo);
            
            logger.LogWarning(
                "Could not determine best content type property for {PropertyName}: Property type {PropertyTypeName} is not supported.",
                propertyInfo.Name,
                propertyType.Name
            );
        }

        return umbracoProperty;
    }

    private static Type ResolvePropertyType(PropertyInfo propertyInfo)
    {
        Type? underlyingType = Nullable.GetUnderlyingType(propertyInfo.PropertyType);
        Type resolvedPropertyType = underlyingType ?? propertyInfo.PropertyType;
        return resolvedPropertyType;
    }

    private async Task<PropertyType> BuildDefaultUmbracoPropertyType(PropertyInfo propertyInfo)
    {
        return new(shortStringHelper, await umbracoDataTypeLoader.Textarea())
        {
            Description = $"WARNING: Fallback property being used; unable find appropriate property type for {propertyInfo.PropertyType.Name}"
        };
    }
    
    private static string PluraliseOpenReferralContentType(string singular)
    {
        string plural = singular.Pluralize();
        if (plural == singular)
            plural = singular + "s"; // Cater for scenarios where singular and plural forms are the same

        return plural;
    }
    
    private string ToAlias(string name) => shortStringHelper.CleanString(name, CleanStringType.CamelCase);

    private static bool IsOpenReferralType(Type type) => type.GetFullNameWithAssembly().StartsWith("FamilyHubs.OR.Umbraco.Models.HSDS");
}