﻿using System.Text.Json;
using AutoMapper;
using FamilyHubs.ServiceDirectory.Core.Helper;
using FamilyHubs.ServiceDirectory.Core.Queries.Dsl;
using FamilyHubs.ServiceDirectory.Core.Queries.Dsl.Condition;
using FamilyHubs.ServiceDirectory.Data.Entities;
using FamilyHubs.ServiceDirectory.Data.Repository;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.ServiceDirectory.Shared.Models;
using FamilyHubs.ServiceDirectory.Shared.ReferenceData.ICalendar;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FamilyHubs.ServiceDirectory.Core.Queries.Services.GetServices;

public class GetServicesCommand : IRequest<PaginatedList<ServiceDto>>
{
    public GetServicesCommand(
        ServiceType? serviceType, ServiceStatusType? status,
        string? districtCode,
        string? ageRangeList,
        double? latitude, double? longitude, double? proximity,
        int? pageNumber, int? pageSize,
        string? text,
        string? serviceDeliveries,
        bool? isPaidFor,
        string? taxonomyIds,
        string? languages,
        bool? canFamilyChooseLocation,
        bool? isFamilyHub,
        string? days)
    {
        ServiceType = serviceType ?? ServiceType.NotSet;
        Status = status ?? ServiceStatusType.NotSet;
        DistrictCode = districtCode;
        if (ageRangeList is not null) AgeRangeList = JsonSerializer.Deserialize<List<int[]>>(ageRangeList);
        Latitude = latitude;
        Longitude = longitude;
        Meters = proximity;
        PageNumber = pageNumber ?? 1;
        PageSize = pageSize ?? 10;
        Text = text;
        ServiceDeliveries = serviceDeliveries;
        IsPaidFor = isPaidFor;
        TaxonomyIds = taxonomyIds;
        Languages = languages;
        CanFamilyChooseLocation = canFamilyChooseLocation;
        IsFamilyHub = isFamilyHub;
        DaysAvailable = days;
    }

    public ServiceType ServiceType { get; }
    public ServiceStatusType Status { get; set; }
    public string? DistrictCode { get; }
    public List<int[]>? AgeRangeList { get; }
    public double? Latitude { get; }
    public double? Longitude { get; }
    public double? Meters { get; }
    public int PageNumber { get; }
    public int PageSize { get; }
    public string? Text { get; }
    public string? ServiceDeliveries { get; }
    public string? DaysAvailable { get; }
    public bool? IsPaidFor { get; }
    public string? TaxonomyIds { get; }
    public string? Languages { get; }
    public bool? CanFamilyChooseLocation { get; }
    public bool? IsFamilyHub { get; }
}

public class GetServicesCommandHandler : IRequestHandler<GetServicesCommand, PaginatedList<ServiceDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly bool _useSqlite;

    public GetServicesCommandHandler(IConfiguration configuration, ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

        _useSqlite = configuration.GetValue<bool>("UseSqlite");
    }

    public async Task<PaginatedList<ServiceDto>> Handle(GetServicesCommand request, CancellationToken cancellationToken)
    {
        if (request.Status == ServiceStatusType.NotSet)
            request.Status = ServiceStatusType.Active;

        var (total, dbServices) = await GetServices(request, cancellationToken);

        var filteredServices = _mapper.Map<List<ServiceDto>>(dbServices);

        filteredServices = SortServicesDto(request, filteredServices);

        filteredServices = ResolveOrganisations(filteredServices);
        
        var result = new PaginatedList<ServiceDto>(filteredServices, total, request.PageNumber, request.PageSize);

        return result;
    }

    private async Task<(int, List<Service>)> GetServices(GetServicesCommand request, CancellationToken cancellationToken)
    {
        var query = new FhQuery("[Services] s", "s.Id Value")
            .Join(FhJoin.Type.Left, "[ServiceAtLocations] sl", "s.Id = sl.ServiceId")
            .Join(FhJoin.Type.Left, "[Locations] l", "sl.LocationId = l.Id")
            .Join(FhJoin.Type.Left, "[Organisations] o", "s.OrganisationId = o.Id")
            .Join(FhJoin.Type.Left, "[ServiceTaxonomies] st", "s.Id = st.ServiceId")
            .Join(FhJoin.Type.Left, "[Languages] la", "s.Id = la.ServiceId")
            .Join(FhJoin.Type.Left, "[Eligibilities] e", "s.Id = e.ServiceId")
            .Join(FhJoin.Type.Left, "[CostOptions] co", "s.Id = co.ServiceId")
            .Join(FhJoin.Type.Left, "[ServiceDeliveries] sd", "s.Id = sd.ServiceId")
            .Join(FhJoin.Type.Left, "[Schedules] ls", "sl.Id = ls.ServiceAtLocationId")
            .Join(FhJoin.Type.Left, "[Schedules] ss", "s.Id = ss.ServiceId")
            .GroupBy("s.Id")
            .SetLimit(FhQueryLimit.FromPage(request.PageNumber, request.PageSize))
            .And(
                new StringCondition("s.Status", "Status", request.Status.ToString())
            ).AndWhen(
                request.ServiceType != ServiceType.NotSet,
                new StringCondition("s.ServiceType", "ServiceType", request.ServiceType.ToString())
            ).AndNotNull(
                request.DistrictCode,
                code => new StringCondition("o.AdminAreaCode", "DistrictCode", code)
            ).AndNotNull(
                request.TaxonomyIds,
                taxIds => new StringCondition("st.TaxonomyId IS NULL").Or(
                    new InCondition("st.TaxonomyId", "TaxonomyIds", taxIds)
                )
            ).AndWhen(
                request.CanFamilyChooseLocation is true,
                new StringCondition("s.CanFamilyChooseDeliveryLocation = 1")
            ).AndNotNull(
                request.Languages,
                langs => new InCondition("la.Code", "LanguageCodes", langs)
            ).AndNotNull(
                request.IsFamilyHub,
                ifh =>
                {
                    var op = ifh ? "" : "NOT ";
                    return new StringCondition(
                        $"{op}EXISTS (SELECT L2.Id FROM [ServiceAtLocations] fh INNER JOIN [Locations] L2 on L2.Id = fh.LocationId AND " +
                        $"L2.LocationTypeCategory = @LocationTypeCategory WHERE fh.ServiceId = s.Id)",
                        new FhParameter("@LocationTypeCategory", LocationTypeCategory.FamilyHub.ToString())
                    );
                }
            ).AndNotNull(
                request.IsPaidFor,
                ipf => new StringCondition("co.ServiceId IS " + (ipf ? "NOT" : "") + " NULL")
            ).AndNotNull(
                request.Text,
                txt => new StringCondition("CHARINDEX(@RequestText, s.Name) > 0", new FhParameter("@RequestText", txt)).Or(
                    new StringCondition("CHARINDEX(@RequestText, s.Description) > 0", new FhParameter("@RequestText", txt))
                )
            ).AndNotNull(
                request.ServiceDeliveries,
                sd => new InCondition("sd.Name", "ServiceServiceDeliveries", sd)
            );

        if (request.AgeRangeList is not null)
        {
            query.And(
                new OrCondition(
                    request.AgeRangeList.Select((ageRange, id) =>
                            new StringCondition($"(@GivenAgeMin{id} >= e.MinimumAge AND @GivenAgeMax{id} <= e.MaximumAge) " +
                                                "OR " +
                                                $"(e.MinimumAge >= @GivenAgeMin{id} AND e.MinimumAge <= @GivenAgeMax{id}) " +
                                                "OR " +
                                                $"(e.MinimumAge <= @GivenAgeMin{id} AND e.MaximumAge <= @GivenAgeMax{id} AND e.MaximumAge >= @GivenAgeMin{id})", 
                                new FhParameter($"@GivenAgeMin{id}", ageRange[0]), 
                                new FhParameter($"@GivenAgeMax{id}", ageRange[1]))
                        )
                        .ToArray<FhQueryCondition>()));
        }

        if (request.DaysAvailable is not null)
        {
            var validDays = request.DaysAvailable
                .Split(",")
                .Where(day => Enum.TryParse(day, out DayCode _))
                .ToArray();
            var concatOperator = _useSqlite ? "||" : "+"; // MS SQL refuses to adopt the standard

            if (validDays.Any())
            {
                query
                    .And(
                        new OrCondition(
                            validDays
                                .Select((day, idx) => new StringCondition($"ls.ByDay LIKE ('%' {concatOperator} @Day{idx} {concatOperator} '%') OR " +
                                                                          $"ss.ByDay LIKE ('%' {concatOperator} @Day{idx} {concatOperator} '%')",
                                    new FhParameter($"@Day{idx}", day)))
                                .ToArray<FhQueryCondition>()
                        )
                    );
            }
        }

        if (request.Longitude is not null && request.Latitude is not null)
        {
            var distanceSql = _useSqlite ?
                $"Distance(l.GeoPoint, GeomFromText('POINT ({request.Longitude.Value} {request.Latitude.Value})', {GeoPoint.WGS84}))" :
                $"l.GeoPoint.STDistance(geography::Point({request.Latitude.Value}, {request.Longitude.Value}, {GeoPoint.WGS84}))";

            if (request.Meters is not null)
            {
                query.And(
                    new StringCondition(
                        $"{distanceSql} < @MaxDistance",
                        new FhParameter("@MaxDistance", request.Meters.Value)
                    )
                );
            }

            query
                .AddFields($"MIN({distanceSql}) dist")
                .AddOrderBy("dist");
        }
        
        query.AddOrderBy("s.Id");

        var pArr = query.AllParameters(_useSqlite);
        var total = await _context.Database.SqlQueryRaw<long>(query.Format(_useSqlite, includeOrderBy: false, includeLimit: false), pArr).CountAsync(cancellationToken);
        var ids = _context.Database.SqlQueryRaw<long>(query.Format(_useSqlite), pArr);
        var joinQuery = _context.Services
            .IgnoreAutoIncludes()
            .AsNoTracking()
            .AsSplitQuery()
            .Where(x => ids.Contains(x.Id))
            .Include(x => x.Taxonomies)
            .Include(x => x.ServiceDeliveries)
            .Include(x => x.Eligibilities)
            .Include(x => x.Languages)
            .Include(x => x.ServiceAreas)
            .Include(x => x.Schedules)
            .Include(x => x.Contacts)
            .Include(x => x.CostOptions)
            .Include(x => x.ServiceAtLocations)
            .ThenInclude(x => x.Schedules)
            .Include("Locations.Schedules")
            .Include("Locations.Contacts");

        return (total, await joinQuery.ToListAsync(cancellationToken));
    }

    private static List<ServiceDto> SortServicesDto(GetServicesCommand request, List<ServiceDto> services)
    {
        if (request.Latitude is not null && request.Longitude is not null)
        {
            foreach (var service in services)
            {
                service.Distance = service.Locations
                    .Min(location => HelperUtility.GetDistance(
                        request.Latitude,
                        request.Longitude,
                        location.Latitude,
                        location.Longitude));
            }

            services = services
                .OrderBy(x => x.Distance)
                .ToList();
        }

        return services;
    }

    private List<ServiceDto> ResolveOrganisations(List<ServiceDto> services)
    {
        var organisationIds = services.Select(x => x.OrganisationId);
        var organisations = _context.Organisations.Where(x => organisationIds.Contains(x.Id)).Select(o => new { o.Id, o.Name }).ToList();
        services.ForEach(s =>
        {
            var organisation = organisations.Find(x => x.Id == s.OrganisationId);
            s.OrganisationName = organisation?.Name ?? string.Empty;
        });
        return services;
    }
}
