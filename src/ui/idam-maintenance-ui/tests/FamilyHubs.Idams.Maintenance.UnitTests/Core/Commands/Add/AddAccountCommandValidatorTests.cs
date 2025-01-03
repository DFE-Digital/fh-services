using FamilyHubs.Idams.Maintenance.Core.Commands.Add;
using FluentAssertions;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Core.Commands.Add;

public class AddAccountCommandValidatorTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void InvalidEmail_Validate_SetsIsValidFalse(string? email)
    {
        var validator = new AddAccountCommandValidator();
        
        var result = validator.Validate(new AddAccountCommand { Email = email!, Name = "Jane Doe "});

        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void NameTooLong_Validate_SetsIsValidFalse()
    {
        var validator = new AddAccountCommandValidator();
        var name = string.Concat(Enumerable.Repeat('a', AddAccountCommandValidator.NameMaxLength + 1));

        var result = validator.Validate(new AddAccountCommand { Email = "jd@temp.org", Name = name });

        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void ValidEmailAndName_Validate_SetsIsValidTrue()
    {
        var validator = new AddAccountCommandValidator();
        
        var result = validator.Validate(new AddAccountCommand { Email = "jd@temp.org", Name = "Jane Doe "});

        result.IsValid.Should().BeTrue();
    }
}