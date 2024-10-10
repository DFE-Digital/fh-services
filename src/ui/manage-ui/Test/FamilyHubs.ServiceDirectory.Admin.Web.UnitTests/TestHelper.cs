using AutoFixture;
using FamilyHubs.ServiceDirectory.Shared.Dto;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using NSubstitute;

namespace FamilyHubs.ServiceDirectory.Admin.Web.UnitTests
{
    internal static class TestHelper
    {
        /// <summary>
        /// Uses NSubstitute to create a HttpContext with a user with the given claims
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static HttpContext GetHttpContext(List<Claim> claims)
        {
            var mockUser = Substitute.For<ClaimsPrincipal>();
            mockUser.Claims.Returns(claims);

            var mockCookies = Substitute.For<IResponseCookies>();

            var mockResponse = Substitute.For<HttpResponse>();
            mockResponse.Cookies.Returns(mockCookies);

            var mock = Substitute.For<HttpContext>();
            mock.User.Returns(mockUser);
            mock.Response.Returns(mockResponse);

            return mock;
        }

        public static OrganisationDto CreateTestOrganisation(long id, long? parentId, OrganisationType organisationType, Fixture fixture)
        {
            var organisation = fixture.Create<OrganisationDto>();
            organisation.Id = id;
            organisation.AssociatedOrganisationId = parentId;
            organisation.OrganisationType = organisationType;
            return organisation;
        }

        public static OrganisationDetailsDto CreateTestOrganisationWithServices(long id, long? parentId, OrganisationType organisationType, Fixture fixture)
        {
            var organisation = fixture.Create<OrganisationDetailsDto>();
            organisation.Id = id;
            organisation.AssociatedOrganisationId = parentId;
            organisation.OrganisationType = organisationType;
            return organisation;
        }
        
        /// <summary>
        /// Uses NSubstitute to capture the argument passed to a method
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public class ArgumentCaptor<T>
        {
            public T Capture()
            {
                return Arg.Is<T>(t => SaveValue(t));
            }

            private bool SaveValue(T t)
            {
                Value = t;
                return true;
            }

            public T? Value { get; private set; }
        }
    }
}
