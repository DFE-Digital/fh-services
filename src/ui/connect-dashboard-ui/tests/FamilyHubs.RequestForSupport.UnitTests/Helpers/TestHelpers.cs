using System.Net;
using FamilyHubs.ReferralService.Shared.Dto;

namespace FamilyHubs.RequestForSupport.UnitTests.Helpers;

// TODO: This is duplicated across projects, when their all updated with NSubstitute, create a shared project for this
public static class TestHelpers
{
    public static HttpClient GetMockClient(string content, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var mockHttpMessageHandler = new CustomHttpMessageHandler(content, statusCode);

        var client = new HttpClient(mockHttpMessageHandler)
        {
            BaseAddress = new Uri("https://localhost")
        };
        return client;
    }
    
    public static ReferralDto GetMockReferralDto()
    {
        return new ReferralDto
        {
            Id = 2,
            ReasonForSupport = "Reason For Support",
            EngageWithFamily = "Engage With Family",
            RecipientDto = new RecipientDto
            {
                Id = 2,
                Name = "Joe Blogs",
                Email = "JoeBlog@email.com",
                Telephone = "078123456",
                TextPhone = "078123456",
                AddressLine1 = "Address Line 1",
                AddressLine2 = "Address Line 2",
                TownOrCity = "Town or City",
                County = "County",
                PostCode = "B30 2TV"
            },
            ReferralUserAccountDto = new UserAccountDto
            {
                Id = 2,
                EmailAddress = "Bob.Referrer@email.com",
                UserAccountRoles = new List<UserAccountRoleDto>
                {
                    new()
                    {
                        UserAccount = new UserAccountDto
                        {
                            EmailAddress = "Bob.Referrer@email.com",
                        },
                        Role = new RoleDto
                        {
                            Name = "LaProfessional"
                        }
                    }
                }
            },
            Status = new ReferralStatusDto
            {
                Name = "New",
                SortOrder = 0
            },
            ReferralServiceDto = new ReferralServiceDto
            {
                Id = 2,
                Name = "Service",
                Description = "Service Description",
                OrganisationDto = new OrganisationDto
                {
                    Id = 2,
                    Name = "Organisation",
                    Description = "Organisation Description",
                }
            }
        };
    }

    /// <summary>
    /// Custom HttpMessageHandler to return a response message with the content
    /// </summary>
    private class CustomHttpMessageHandler(string content, HttpStatusCode statusCode) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var responseMessage = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(content),
                RequestMessage = request
            };

            return Task.FromResult(responseMessage);
        }
    }
}