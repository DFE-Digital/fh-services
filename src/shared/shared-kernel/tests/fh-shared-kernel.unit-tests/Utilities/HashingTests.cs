using System.Text.Json;
using FamilyHubs.SharedKernel.Utilities;

namespace FamilyHubs.SharedKernel.UnitTests.Utilities;

// TODO: This may not be a necessary file as it's only testing dotnet algos. Only value test is we are returning not 0
public class HashingTests
{
    [Fact]
    public void ComputeHash_ShouldReturnHash()
    {
        // Arrange
        var value = new MockHashClass
        {
            Id = 1,
            Name = "test 1",
            Description = "test description 1",
            HasClassTwo = new MockHasClassTwo
            {
                Id = 2,
                SomeValue = "test 2",
                SomeLongValue = 123456789
            }
        };
        var valueTwo = new MockHashClass
        {
            Id = 1,
            Name = "test 1",
            Description = "test description 1",
            HasClassTwo = new MockHasClassTwo
            {
                Id = 2,
                SomeValue = "test 2",
                SomeLongValue = 123456789
            }
        };
        var valueOneJson = JsonSerializer.Serialize(value);
        var valueTwoJson = JsonSerializer.Serialize(valueTwo);
        
        
        // Act
        var resultOne = HashingAlgorithms.ComputeXxHashToLong64(valueOneJson);
        var resultTwo = HashingAlgorithms.ComputeXxHashToLong64(valueTwoJson);
        
        // Assert
        Assert.Equal(resultOne.ToString(), resultTwo.ToString());
    }

    private class MockHashClass
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public string? Thing { get; set; }
        public MockHasClassTwo? HasClassTwo { get; set; }
    }

    private class MockHasClassTwo
    {
        public int Id { get; set; }
        public required string SomeValue { get; set; }
        public long SomeLongValue { get; set; }
    }
}