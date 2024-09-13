using System;
using System.ComponentModel;
using FamilyHubs.SharedKernel.Enums;

namespace FamilyHubs.SharedKernel.UnitTests.Enums;

public class EnumExtensionsTests
{
    private enum TestEnum
    {
        [Description("Test Description")]
        [Example("Test Value")]
        TestValue
    }

    private class ExampleAttribute : Attribute
    {
        public string Value { get; }

        public ExampleAttribute(string value)
        {
            Value = value;
        }
    }

    [Fact]
    public void GetAttribute_ReturnsExpectedAttribute()
    {
        // Arrange
        var enumValue = TestEnum.TestValue;

        // Act
        var attribute = enumValue.GetAttribute<ExampleAttribute>();

        // Assert
        Assert.NotNull(attribute);
        Assert.Equal("Test Value", attribute.Value);
    }

    [Fact]
    public void ToDescription_ReturnsExpectedDescription()
    {
        // Arrange
        var enumValue = TestEnum.TestValue;

        // Act
        var description = enumValue.ToDescription();

        // Assert
        Assert.Equal("Test Description", description);
    }
}