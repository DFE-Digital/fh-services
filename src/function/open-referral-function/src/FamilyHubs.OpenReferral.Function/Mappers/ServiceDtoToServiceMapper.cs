using AutoMapper;
using AutoMapper.EquivalencyExpression;
using FamilyHubs.OpenReferral.Function.Models;
using FamilyHubs.SharedKernel.OpenReferral.Entities;
using Google.Protobuf.WellKnownTypes;
using Attribute = FamilyHubs.SharedKernel.OpenReferral.Entities.Attribute;

namespace FamilyHubs.OpenReferral.Function.Mappers;

public class ServiceDtoToServiceMapper : Profile
{
    
    public ServiceDtoToServiceMapper()
    {
        
        CreateMap<OrganizationDto, Organization>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<OrganizationIdentifierDto, OrganizationIdentifier>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<ServiceDto, Service>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<ServiceAreaDto, ServiceArea>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<ServiceAtLocationDto, ServiceAtLocation>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<AccessibilityDto, Accessibility>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<AddressDto, Address>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<ContactDto, Contact>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<PhoneDto, Phone>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<ScheduleDto, Schedule>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<AttributeDto, Attribute>();
        CreateMap<ProgramDto, SharedKernel.OpenReferral.Entities.Program>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<RequiredDocumentDto, RequiredDocument>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<ScheduleDto, Schedule>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<CostOptionDto, CostOption>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<FundingDto, Funding>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<LanguageDto, Language>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<LocationDto, Location>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<TaxonomyDto, Taxonomy>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<TaxonomyTermDto, TaxonomyTerm>()
            .EqualityComparison((dto, o) => dto.OrId == o.OrId);
        CreateMap<MetadataDto, Metadata>();
        
    }
}