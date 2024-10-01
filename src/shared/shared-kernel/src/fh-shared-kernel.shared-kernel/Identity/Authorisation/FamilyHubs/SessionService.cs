using FamilyHubs.SharedKernel.Identity.Exceptions;
using FamilyHubs.SharedKernel.Identity.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FamilyHubs.SharedKernel.Identity.Authorisation.FamilyHubs
{
    public interface ISessionService
    {
        Task CreateSession(UserSession userSession);
        Task<bool> IsSessionActive(string sid);
        Task EndSession(string sid);
        Task RefreshSession(string sid);
        Task EndAllUserSessions(string email);
    }

    public class SessionService : ISessionService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<SessionService> _logger;

        public SessionService(ILogger<SessionService> logger, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory?.CreateClient(nameof(FamilyHubsClaims))!;
            _logger = logger;
        }

        public async Task CreateSession(UserSession userSession)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_httpClient.BaseAddress}api/UserSession"),
                Content = new StringContent(JsonConvert.SerializeObject(userSession), Encoding.UTF8, "application/json")
            };

            _logger.LogInformation("Calling Idams to create session");
            using var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Call to Idams to created new session failed with {StatusCode}", response.StatusCode);
            }

            response.EnsureSuccessStatusCode();

        }

        public async Task<bool> IsSessionActive(string sid)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_httpClient.BaseAddress}api/UserSession/{sid}")
            };

            _logger.LogInformation("Calling Idams to get session");
            using var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Call to Idams to get session failed with {StatusCode}", response.StatusCode);
            }

            response.EnsureSuccessStatusCode();

            if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return false;
            }

            return true;
        }

        public async Task EndSession(string sid)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{_httpClient.BaseAddress}api/UserSession/{sid}"),
                Content = new StringContent(JsonConvert.SerializeObject(sid), Encoding.UTF8, "application/json")
            };

            _logger.LogInformation("Calling Idams to delete session");
            using var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Call to Idams to delete session failed with {StatusCode}", response.StatusCode);
            }

            response.EnsureSuccessStatusCode();

        }

        public async Task EndAllUserSessions(string email)
        {
            var emailEncoded = HttpUtility.UrlEncode(email);
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{_httpClient.BaseAddress}api/UserSession/DeleteAllUserSessions?email={emailEncoded}"),
            };

            _logger.LogInformation("Calling Idams to delete all user sessions");
            using var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Call to Idams to delete all user sessions failed with {StatusCode}", response.StatusCode);
            }

            response.EnsureSuccessStatusCode();
        }

        public async Task RefreshSession(string sid)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_httpClient.BaseAddress}api/UserSession/{sid}"),
                Content = new StringContent(JsonConvert.SerializeObject(sid), Encoding.UTF8, "application/json")
            };

            _logger.LogInformation("Calling Idams to refresh session");
            using var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Call to Idams to get session failed with {StatusCode}", response.StatusCode);
            }

        }
    }
}
