using FamilyHubs.Notification.Api.Contracts;
using FluentAssertions;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Xunit.Abstractions;
// ReSharper disable NotAccessedField.Local

namespace FamilyHubs.Notification.FunctionalTests;

//https://stackoverflow.com/questions/7138935/xunit-net-does-not-capture-console-output

public class WhenUsingNotifications : BaseWhenUsingOpenReferralApiUnitTests
{
    private readonly ITestOutputHelper _output;

    public WhenUsingNotifications(ITestOutputHelper output)
    {
        _output = output;
    }

    public class ConsoleWriter : StringWriter
    {
        private readonly ITestOutputHelper _output;
        public ConsoleWriter(ITestOutputHelper output)
        {
            _output = output;
        }

        public override void WriteLine(string? value)
        {
            _output.WriteLine(value);
        }
    }

    // Uncomment to run locally
    /*
    [Theory]
    [InlineData("ProfessionalAcceptRequest")]
    [InlineData("ProfessionalDecineRequest")]
    [InlineData("ProfessionalSentRequest")]
    [InlineData("VcsNewRequest")]
    public async Task ThenSendEmailNotificationToUser(string key)
    {
        if (!IsRunningLocally() || Client == null)
        {
            // Skip the test if not running locally
            Assert.True(true, "Test skipped because it is not running locally.");
            return;
        }

        Console.SetOut(new ConsoleWriter(_output));
        if (!_templates!.ContainsKey(key))
        {
            return;
        }

        var command = new MessageDto
        {
            ApiKeyType = ApiKeyType.ConnectKey,
            NotificationEmails = new List<string> { _emailRecipient! },
            TemplateId = _templates[key],
            TemplateTokens = new Dictionary<string, string>
            {
                { "reference number", "0001" },
                { "RequestNumber", "0001" },
                { "ServiceName", "ServiceName" },
                { "ViewConnectionRequestUrl",  "wwww.someurl.com"},
                { "Name of service", "Special Test Service" },
                { "link to specific connection request", "wwww.someurl.com" }
            }
        };


        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(Client.BaseAddress + "api/notify"),
            Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json"),
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{new JwtSecurityTokenHandler().WriteToken(_token)}");

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var stringResult = await response.Content.ReadAsStringAsync();
        bool.TryParse(stringResult, out var result);

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        result.Should().BeTrue();
    }
    */

    [Fact]
    public async Task ThenGetNotificationsList()
    {
        if (!IsRunningLocally() || Client == null)
        {
            // Skip the test if not running locally
            Assert.True(true, "Test skipped because it is not running locally.");
            return;
        }

        SeedDatabase();

        var expected = new PaginatedList<MessageDto>(GetMapper().Map<List<MessageDto>>(GetNotificationList()),2,1,10);

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(Client!.BaseAddress + $"api/notify"),
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{new JwtSecurityTokenHandler().WriteToken(Token)}");

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var retVal = await JsonSerializer.DeserializeAsync<PaginatedList<MessageDto>>(await response.Content.ReadAsStreamAsync(), options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        retVal.Should().NotBeNull();
        retVal.Should().BeEquivalentTo(expected);

    }

    [Fact]
    public async Task ThenGetNotificationById()
    {
        if (!IsRunningLocally() || Client == null)
        {
            // Skip the test if not running locally
            Assert.True(true, "Test skipped because it is not running locally.");
            return;
        }

        SeedDatabase();

        var expected = GetMapper().Map<MessageDto>(GetNotificationList().First(x => x.Id == 1));

        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(Client!.BaseAddress + $"api/notify/1"),
        };

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue($"Bearer", $"{new JwtSecurityTokenHandler().WriteToken(Token)}");

        using var response = await Client.SendAsync(request);

        response.EnsureSuccessStatusCode();

        var retVal = await JsonSerializer.DeserializeAsync<MessageDto>(await response.Content.ReadAsStreamAsync(), options: new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        retVal.Should().NotBeNull();
        retVal.Should().BeEquivalentTo(expected, options => options.Excluding(x => x.Created));

    }
}
