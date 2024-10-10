using System.Text;
using FamilyHubs.ServiceDirectory.Shared.ReferenceData.ICalendar;

namespace FamilyHubs.Referral.Core.ApiClients;

// TODO: Move to a shared library?
public class GetServicesUrlBuilder
{
    private readonly List<string> _urlParameter = new();
    public GetServicesUrlBuilder WithServiceType(string serviceType)
    {
        _urlParameter.Add($"serviceType={serviceType}");
        return this;
    }
    public GetServicesUrlBuilder WithStatus(string status)
    {
        _urlParameter.Add($"status={status}");
        return this;
    }

    public GetServicesUrlBuilder WithDistrictCode(string code)
    {
        _urlParameter.Add($"districtCode={code}");
        return this;
    }

    public GetServicesUrlBuilder WithEligibility(int minimum_age, int maximum_age)
    {
        _urlParameter.Add( $"minimum_age={minimum_age}&maximum_age={maximum_age}" );
        return this;
    }

    public GetServicesUrlBuilder WithProximity(double latitude, double longitude, double proximity)
    {
        _urlParameter.Add($"latitude={latitude}&longitude={longitude}&proximity={proximity}");
        return this;
    }

    public GetServicesUrlBuilder WithPage(int pageNumber, int pageSize)
    {
        _urlParameter.Add($"pageNumber={pageNumber}&pageSize={pageSize}");
        return this;
    }

    public GetServicesUrlBuilder WithSearchText(string searchText)
    {
        _urlParameter.Add($"text={searchText}");
        return this;
    }

    public GetServicesUrlBuilder WithDelimitedSearchDeliveries(string serviceDeliveries)
    {
        _urlParameter.Add($"serviceDeliveries={serviceDeliveries}");
        return this;
    }

    public GetServicesUrlBuilder WithDelimitedTaxonomies(string taxonmyIds)
    {
        _urlParameter.Add($"taxonmyIds={taxonmyIds}");
        return this;
    }

    public GetServicesUrlBuilder WithFamilyHub(bool isFamilyHub)
    {
        _urlParameter.Add($"isFamilyHub={isFamilyHub}");
        return this;
    }

    public GetServicesUrlBuilder WithMaxFamilyHubs(int maxFamilyHubs)
    {
        _urlParameter.Add($"maxFamilyHubs={maxFamilyHubs}");
        return this;
    }

    public GetServicesUrlBuilder WithDaysAvailable(params DayCode[] codes)
    {
        _urlParameter.Add($"days={string.Join(",", codes)}");
        return this;
    }

    public string Build() =>
        _urlParameter.Aggregate(new StringBuilder(), (builder, param) =>
        {
            builder.Append(builder.Length == 0 ? "?" : "&");
            builder.Append(param);
            return builder;
        }).ToString();
}
