using FamilyHubs.SharedKernel.Factories;
using FamilyHubs.SharedKernel.Services.Sanitizers;

namespace FamilyHubs.SharedKernel.UnitTests.Services;

public class SanitizerTests
{
    private readonly StringSanitizer _sanitizer = new();

    [Theory]
    [InlineData("<br />Test", "Test")]
    [InlineData("<br />Test&nbsp;", "Test ")]
    [InlineData("<div>Test</div>", "Test")]
    [InlineData("<div><p>Test</p></div>", "Test")]
    [InlineData("<div><p><span>Test<span></p> and test</div>", "Test and test")]
    public void ShouldRemoveHtml(string input, string expected)
    {
        var result = SanitizerFactory.CreateDedsTextSanitizer().Sanitize(input);
        
        Assert.Equal(expected, result);
    }
    
    [Theory]
    [InlineData("<script>alert('Test')</script>", "")]
    [InlineData("<div onclick='alert(\"Test\")'>Click me</div>", "Click me")]
    [InlineData("<a href='javascript:alert(\"Test\")'>Link</a>", "Link")]
    [InlineData("<img src='image.jpg' onload='alert(\"Test\")' />", "")]
    [InlineData("<div onmouseover='alert(\"Test\")'>Hover over me</div>", "Hover over me")]
    [InlineData("<button onclick='alert(\"Test\")'>Click me</button>", "Click me")]
    [InlineData("<div><script>alert('Nested')</script> script</div>", " script")]
    [InlineData("<div><a href='javascript:alert(\"Test\")'>Link</a> with script</div>", "Link with script")]
    
    public void ShouldRemoveJavascript(string input, string expected)
    {
        var result = SanitizerFactory.CreateDedsTextSanitizer().Sanitize(input);
        
        Assert.Equal(expected, result);
    }
    
    [Fact]
    public void ShouldSanitizeClass_RemovingHtmlAndJavascriptOnStringProperties()
    {
        // Arrange
        var guid = Guid.NewGuid().ToString();
        var service = new MockClass()
        {
            Name = "<div>Test</div>",
            Description = "<div><p>Test this</p> description <button onClick={someFunction()}>with no<script>alert('bad actor')</script> js</button></div>",
            Url = "<div>www.testexample.com</div>",
            Email = "test@example.com",
            Age = 20,
            MockClassTwo = new MockClassTwo(){Id = $"<div>{guid}</div>", 
                MockClassThree = new MockClassThree()
                {
                    Name = "<div>Test from mock three</div>"
                }}
        };
        
        // Act
        var sanitizer = SanitizerFactory.CreateDedsTextSanitizer();
        var result = sanitizer.Sanitize(service);
        
        // Assert
        Assert.Equal("Test", result.Name);
        Assert.Equal("Test this description with no js", result.Description);
        Assert.Equal("www.testexample.com", result.Url);
        Assert.Equal("test@example.com", result.Email);
        Assert.Equal(20.ToString(), result.Age.ToString());
        Assert.Equal(guid, result.MockClassTwo!.Id);
        Assert.Equal("Test from mock three", result.MockClassTwo!.MockClassThree!.Name);
        
    }

    private class MockClass
    {
        public required string Name { get; set; }
        public required string Description { get; init; } // Checking that init property can be sanitized
        public required string Url { get; init; }
        public string? Email { get; set; }
        public int Age { get; set; }
        public string NotStandard => Name; // Checks that sanitizer handles non settable properties
        
        public MockClassTwo? MockClassTwo { get; set; }
    }

    private class MockClassTwo
    {
        public required string Id { get; set; }
        
        public MockClassThree? MockClassThree { get; set; }
    }
    
    private class MockClassThree
    {
        public required string Name { get; set; }
    }
}