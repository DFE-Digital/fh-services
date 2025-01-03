using FamilyHubs.Idams.Maintenance.Core.Queries.GetAccount;
using FluentAssertions;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Core.Queries.GetAccount;

public class GetAccountByEmailCommandValidatorTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void NullEmail_Validate_SetsIsValidFalse(string? email)
    {
        var validator = new GetAccountByEmailCommandValidator();
        
        var result = validator.Validate(new GetAccountByEmailCommand(email!));

        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void ValidEmail_Validate_SetsIsValidTrue()
    {
        var validator = new GetAccountByEmailCommandValidator();
        
        var result = validator.Validate(new GetAccountByEmailCommand("jd@temp.org"));

        result.IsValid.Should().BeTrue();
    }
}