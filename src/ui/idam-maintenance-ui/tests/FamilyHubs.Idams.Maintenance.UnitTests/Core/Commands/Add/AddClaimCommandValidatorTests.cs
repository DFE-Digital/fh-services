using FamilyHubs.Idams.Maintenance.Core.Commands.Add;
using FluentAssertions;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Core.Commands.Add;

public class AddClaimCommandValidatorTests
{
    [Fact]
    public void InvalidAccountId_Validate_SetsIsValidFalse()
    {
        var validator = new AddClaimCommandValidator();
        
        var result = validator.Validate(new AddClaimCommand { AccountId = 0, Name = "test", Value = "test-value"});

        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void NameTooLong_Validate_SetsIsValidFalse()
    {
        var validator = new AddClaimCommandValidator();
        var name = string.Concat(Enumerable.Repeat('a', AddClaimCommandValidator.NameMaxLength + 1));

        var result = validator.Validate(new AddClaimCommand { AccountId = 100, Name = name, Value = "test-value"});

        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void ValueTooLong_Validate_SetsIsValidFalse()
    {
        var validator = new AddClaimCommandValidator();
        var value = string.Concat(Enumerable.Repeat('a', AddClaimCommandValidator.ValueMaxLength + 1));

        var result = validator.Validate(new AddClaimCommand { AccountId = 100, Name = "test", Value = value });

        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void ValidClaimCommand_Validate_SetsIsValidTrue()
    {
        var validator = new AddClaimCommandValidator();
        
        var result = validator.Validate(new AddClaimCommand { AccountId = 100, Name = "test", Value = "test-value" });

        result.IsValid.Should().BeTrue();
    }
}