using System.Collections.Immutable;
using FamilyHubs.SharedKernel.Razor.ErrorNext;
using FamilyHubs.SharedKernel.Services.Postcode.Model;

namespace FamilyHubs.ServiceDirectory.Web.Errors;

public static class PossibleErrors
{
    public static readonly ImmutableDictionary<int, PossibleError> All = ImmutableDictionary
        .Create<int, PossibleError>()
        .Add(PostcodeError.NoPostcode, "You need to enter a postcode")
        .Add(PostcodeError.InvalidPostcode, "Your postcode is not recognised - try another one")
        .Add(PostcodeError.PostcodeNotFound, "You need to enter a valid postcode")
        ;
}
