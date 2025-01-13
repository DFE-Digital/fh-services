using System.Net;
using System.Text.Json;
using FamilyHubs.Idams.Maintenance.Core.ApiClient;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Core.ApiClient;

public class ServiceDirectoryClientTests
{
    [Fact]
    public async Task GetOrganisations()
    {
        var httpClient = new HttpClient(new MyDelegatingHandler(
            new OrganisationDto
            {
                Id = 1, 
                Name = "Tower Hamlets", 
                Description = "Tower Hamlets organisation", 
                AdminAreaCode = "E1", 
                OrganisationType = OrganisationType.LA
            }, 
            new OrganisationDto
            {
                Id = 2, 
                Name = "Somerset", 
                Description = "Somerset organisation",
                AdminAreaCode = "TA1", 
                OrganisationType = OrganisationType.LA
            }))
        {
            BaseAddress = new Uri("https://localhost/")
        };
        var logger = Substitute.For<ILogger<ServiceDirectoryClient>>();
        var serviceDirectoryClient = new ServiceDirectoryClient(httpClient, logger);
        
        var result = await serviceDirectoryClient.GetOrganisations();
        
        result.Should().Contain(o => o.Id == 1);
        result.Should().Contain(o => o.Id == 2);
    }

    private class MyDelegatingHandler : DelegatingHandler
    {
        private readonly List<OrganisationDto> _organisationDtoList;
        
        public MyDelegatingHandler(params OrganisationDto[] organisationDtos)
        {
            _organisationDtoList = organisationDtos.ToList();    
        }
        
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var uri = request.RequestUri?.ToString();

            if (uri?.EndsWith("api/organisations") == true)
            {
                var json = JsonSerializer.Serialize(_organisationDtoList);
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent(json) });
            }
            
            throw new InvalidOperationException("Unable to match request URI.");
        }
    }
}