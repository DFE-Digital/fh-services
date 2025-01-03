using FamilyHubs.Idams.Maintenance.Core.Queries.GetAccountClaims;
using FluentAssertions;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Core.Queries.GetAccountClaims;

public class GetAccountClaimsByIdCommandValidatorTests
{
    [Fact]
    public void InvalidAccountId_Validate_SetsIsValidFalse()
    {
        var validator = new GetAccountClaimsByIdCommandValidator();
        
        var result = validator.Validate(new GetAccountClaimsByIdCommand{ AccountId = 0 });

        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void ValidAccountId_Validate_SetsIsValidTrue()
    {
        var validator = new GetAccountClaimsByIdCommandValidator();
        
        var result = validator.Validate(new GetAccountClaimsByIdCommand{ AccountId = 1 });

        result.IsValid.Should().BeTrue();
    }
}