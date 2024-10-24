using FamilyHubs.ServiceDirectory.Core.Distance;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Web.Models;

namespace FamilyHubs.ServiceDirectory.Web.Mappers;

public static class LocationMapper
{
    public static IEnumerable<Location> ToModel(IEnumerable<LocationDto> locations)
    {
        return locations.Select(ToModel);
    }

    private static Location ToModel(LocationDto locationDto)
    {
        return new Location(
            Name: locationDto.Name,
            Address:
            [
                locationDto.Address1,
                locationDto.Address2,
                locationDto.City,
                locationDto.PostCode,
                locationDto.StateProvince,
                locationDto.Country
            ],
            MoreDetails: locationDto.Description,
            Distance: locationDto.Distance is not null ? DistanceConverter.MetersToMiles(locationDto.Distance.Value) : null);
    }
}