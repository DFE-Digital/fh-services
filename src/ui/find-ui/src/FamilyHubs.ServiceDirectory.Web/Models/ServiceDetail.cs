namespace FamilyHubs.ServiceDirectory.Web.Models;

public enum ServiceDetailType
{
    Service,
    Location
}

public abstract record ServiceDetail(double? Distance, ServiceDetailType ServiceDetailType);