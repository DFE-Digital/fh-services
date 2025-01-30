using FamilyHubs.SharedKernel.OpenReferral.Entities;
using FamilyHubs.SharedKernel.Utilities;

namespace FamilyHubs.SharedKernel.UnitTests.Utilities;

public class ProfanityCheckerTests
{
    [Fact]
    public void ShouldReturnTrue_WhenClassContainsAnyProfanity()
    {
        // Arrange
        var testClass = new MockClass
        {
            Id = "This has no profanity",
            MockClassTwo = new MockClassTwo
            {
                Description = "This has profanity weirdo"
            }
        };
        
        // Act
        var result = ProfanityChecker.HasProfanity(testClass);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void ShouldReturnTrue_WhenClassContainsAnyProfanityInNestedLists()
    {
        // Arrange
        var testClass = new MockClass
        {
            Id = "This has no profanity",
            MockClassTwo = new MockClassTwo
            {
                Description = "This has profanity",
            },
            ListOfMockClassTwo = new[]
            {
                new MockClassTwo
                {
                    Description = "This has profanity weirdo"
                }
            }
        };
        
        // Act
        var result = ProfanityChecker.HasProfanity(testClass);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public void ShouldReturnFalse_WhenClassContainsNoProfanity()
    {
        // Arrange
        var testClass = new MockClass
        {
            Id = "This has no profanity",
            MockClassTwo = new MockClassTwo
            {
                Description = "This also has no profanity"
            }
        };
        
        // Act
        var result = ProfanityChecker.HasProfanity(testClass);
        
        // Assert
        Assert.False(result);
    }
    
    private class MockClass
    {
        public required string Id { get; init; }
        public required MockClassTwo MockClassTwo { get; set; }
        public MockClassTwo[] ListOfMockClassTwo { get; set; } = [];
        public MockClassTwo? NullableMockClassTwo { get; set; }
    }
    
    private class MockClassTwo
    {
        public required string Description { get; set; }
    }
}