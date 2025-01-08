using FamilyHubs.Idams.Maintenance.Core.Commands.Add;
using FluentAssertions;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Core.Commands.Add;

public class AddUserSessionCommandValidatorTests
{
    [Fact]
    public void EmailTooLong_Validate_SetsIsValidFalse()
    {
        var validator = new AddUserSessionCommandValidator();
        var email = string.Concat(Enumerable.Repeat('a', AddUserSessionCommandValidator.EmailMaxLength + 1));

        var result = validator.Validate(new AddUserSessionCommand { Sid = "100", Email = email });

        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void ValueTooLong_Validate_SetsIsValidFalse()
    {
        var validator = new AddUserSessionCommandValidator();
        var sid = string.Concat(Enumerable.Repeat('a', AddUserSessionCommandValidator.SidMaxLength + 1));

        var result = validator.Validate(new AddUserSessionCommand { Sid = sid, Email = "jd@temp.org" });

        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void ValidClaimCommand_Validate_SetsIsValidTrue()
    {
        var validator = new AddUserSessionCommandValidator();
        
        var result = validator.Validate(new AddUserSessionCommand { Sid = "100", Email = "jd@temp.org" });

        result.IsValid.Should().BeTrue();
    }
}