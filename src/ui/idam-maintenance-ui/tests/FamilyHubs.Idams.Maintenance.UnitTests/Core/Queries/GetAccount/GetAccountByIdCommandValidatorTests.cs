using FamilyHubs.Idams.Maintenance.Core.Queries.GetAccount;
using FluentAssertions;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Core.Queries.GetAccount;

public class GetAccountByIdCommandValidatorTests
{
    [Fact]
    public void EmptyId_Validate_SetsIsValidFalse()
    {
        var validator = new GetAccountByIdCommandValidator();
        
        var result = validator.Validate(new GetAccountByIdCommand { Id = 0 });
        
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void ValidId_Validate_SetsIsValidTrue()
    {
        var validator = new GetAccountByIdCommandValidator();
        
        var result = validator.Validate(new GetAccountByIdCommand { Id = 10 });
        
        result.IsValid.Should().BeTrue();
    }
}