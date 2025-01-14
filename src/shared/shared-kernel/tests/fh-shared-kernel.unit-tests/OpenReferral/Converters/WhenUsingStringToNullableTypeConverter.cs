using System.Text.Json;
using System.Text.Json.Serialization;
using FamilyHubs.SharedKernel.OpenReferral.Converters;

namespace FamilyHubs.SharedKernel.UnitTests.OpenReferral.Converters;

public class WhenUsingStringToNullableTypeConverter
{
    
    [Fact]
    public void ShouldReturnGuid_WhenGuidIsAStringInJson()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var jsonData = $"{{\"id\": \"{guid}\"}}";

        // Act
        var result = JsonSerializer.Deserialize<MyMockData>(jsonData);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(guid, result.Id);
    }
    
    [Fact]
    public void ShouldReturnNull_WhenGuidIsAEmptyStringInJson()
    {
        // Arrange
        var jsonData = "{\"id\": \"\"}";

        // Act
        var result = JsonSerializer.Deserialize<MyMockData>(jsonData);

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.Id);
    }
    
    [Fact]
    public void ShouldReturnDateTime_WhenStringHasADateTime()
    {
        // Arrange
        var jsonData = $"{{\"date\": \"2024-11-14T16:53:16.997\"}}";

        // Act
        var result = JsonSerializer.Deserialize<MyMockData>(jsonData);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Date);
        var expectedDate = "2024-11-14";
        var resultDate = $"{result.Date.Value.Year}-{result.Date.Value.Month}-{result.Date.Value.Day}";
        Assert.Equal(expectedDate, resultDate);
    }
    
    [Fact]
    public void ShouldReturnNull_WhenStringHasNoDateTime()
    {
        // Arrange
        var jsonData = $"{{\"date\": \"\"}}";

        // Act
        var result = JsonSerializer.Deserialize<MyMockData>(jsonData);

        // Assert
        Assert.NotNull(result);
        Assert.Null(result.Date);
    }
    
    [Fact]
    public void ShouldReturnTimeSpan_WhenStringHasATimeSpan()
    {
        // Arrange
        var jsonData = $"{{\"time\": \"16:53:16.997\"}}";

        // Act
        var result = JsonSerializer.Deserialize<MyMockData>(jsonData);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Time);
    }
    
    
    private class MyMockData
    {
        [JsonPropertyName("id")]
        [JsonConverter(typeof(StringToNullableTypeConverter))]
        public Guid? Id { get; init; }
        
        [JsonPropertyName("date")]
        [JsonConverter(typeof(StringToNullableTypeConverter))]
        public DateTime? Date { get; init; }
        
        [JsonPropertyName("time")]
        public TimeSpan? Time { get; init; }
    }
    
    
}