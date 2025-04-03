using FamilyHubs.ServiceDirectory.Shared.Enums;

namespace FamilyHubs.ServiceDirectory.Core.Queries.Services.GetServices;

public class GetServicesCommandBuilder
{
    private ServiceType _serviceType = ServiceType.NotSet;
    private ServiceStatusType _status = ServiceStatusType.NotSet;
    private string? _districtCode;
    private bool? _allChildrenYoungPeople;
    private int? _givenAge;
    private double? _latitude;
    private double? _longitude;
    private double? _meters;
    private int _pageNumber = 1;
    private int _pageSize = 10;
    private string? _text;
    private string? _serviceDeliveries;
    private string? _daysAvailable;
    private bool? _isPaidFor;
    private string? _taxonomyIds;
    private string? _languages;
    private bool? _canFamilyChooseLocation;
    private bool? _isFamilyHub;

    public GetServicesCommandBuilder WithServiceType(ServiceType serviceType)
    {
        _serviceType = serviceType;
        return this;
    }

    public GetServicesCommandBuilder WithServiceStatusType(ServiceStatusType status)
    {
        _status = status;
        return this;
    }

    public GetServicesCommandBuilder WithDistrictCode(string districtCode)
    {
        _districtCode = districtCode;
        return this;
    }

    public GetServicesCommandBuilder WithAge(bool allChildrenYoungPeople, int givenAge)
    {
        _allChildrenYoungPeople = allChildrenYoungPeople;
        _givenAge = givenAge;
        return this;
    }

    public GetServicesCommandBuilder WithLocation(double lat, double lon, double distance)
    {
        _latitude = lat;
        _longitude = lon;
        _meters = distance;
        return this;
    }

    public GetServicesCommandBuilder WithPage(int pageNumber, int pageSize)
    {
        _pageNumber = pageNumber;
        _pageSize = pageSize;
        return this;
    }

    public GetServicesCommandBuilder WithText(string text)
    {
        _text = text;
        return this;
    }

    public GetServicesCommandBuilder WithServiceDeliveries(string serviceDeliveries)
    {
        _serviceDeliveries = serviceDeliveries;
        return this;
    }

    public GetServicesCommandBuilder WithDaysAvailable(string daysAvailable)
    {
        _daysAvailable = daysAvailable;
        return this;
    }

    public GetServicesCommandBuilder WithPaidFor(bool isPaidFor)
    {
        _isPaidFor = isPaidFor;
        return this;
    }

    public GetServicesCommandBuilder WithTaxonomies(string taxonomies)
    {
        _taxonomyIds = taxonomies;
        return this;
    }

    public GetServicesCommandBuilder WithLanguages(string languages)
    {
        _languages = languages;
        return this;
    }

    public GetServicesCommandBuilder WithCanFamilyChooseLocation(bool canFamilyChooseLocation)
    {
        _canFamilyChooseLocation = canFamilyChooseLocation;
        return this;
    }

    public GetServicesCommandBuilder WithFamilyHub(bool isFamilyHub)
    {
        _isFamilyHub = isFamilyHub;
        return this;
    }

    public GetServicesCommand Build() => new(
        _serviceType,
        _status,
        _districtCode,
        _allChildrenYoungPeople,
        _givenAge,
        _latitude, _longitude, _meters,
        _pageNumber, _pageSize,
        _text,
        _serviceDeliveries,
        _isPaidFor,
        _taxonomyIds,
        _languages,
        _canFamilyChooseLocation,
        _isFamilyHub,
        _daysAvailable
    );
}