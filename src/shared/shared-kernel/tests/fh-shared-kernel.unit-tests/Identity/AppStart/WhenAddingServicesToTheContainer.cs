using System;
using System.Collections.Generic;
using System.Linq;
using FamilyHubs.SharedKernel.GovLogin.AppStart;
using FamilyHubs.SharedKernel.Identity.Authentication.Gov;
using FamilyHubs.SharedKernel.Identity.Authorisation;
using FamilyHubs.SharedKernel.UnitTests.Identity.TestHelpers;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FamilyHubs.SharedKernel.UnitTests.Identity.AppStart
{
    public class WhenAddingServicesToTheContainer
    {
        [Theory]
        [InlineData(typeof(IOidcService))]
        [InlineData(typeof(IAzureIdentityService))]
        [InlineData(typeof(IJwtSecurityTokenService))]
        [InlineData(typeof(ICustomClaims))]
        public void Then_The_Dependencies_Are_Correctly_Resolved(Type toResolve)
        {
            var serviceCollection = new ServiceCollection();
            SetupServiceCollection(serviceCollection);

            var provider = serviceCollection.BuildServiceProvider();

            var type = provider.GetService(toResolve);

            Assert.NotNull(type);
        }

        [Fact]
        public void Then_Resolves_Authorization_Handlers()
        {
            var serviceCollection = new ServiceCollection();
            SetupServiceCollection(serviceCollection);
            var provider = serviceCollection.BuildServiceProvider();

            var type = provider.GetServices(typeof(IAuthorizationHandler)).ToList();

            Assert.NotNull(type);
            Assert.IsType<AuthorizationHandler>(type.Single());
        }


        private static void SetupServiceCollection(IServiceCollection serviceCollection)
        {
            var configuration = FakeConfiguration.GetConfiguration();
            var configManager = new ConfigurationManager();
            configManager.AddConfiguration(configuration);

            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddAndConfigureGovUkAuthentication(configManager);
        }

        public class TestCustomClaims : ICustomClaims
        {
            public Task<IEnumerable<Claim>> GetClaims(TokenValidatedContext tokenValidatedContext)
            {
                throw new NotImplementedException();
            }

            public Task<IEnumerable<Claim>> RefreshClaims(string email, List<Claim> currentClaims)
            {
                throw new NotImplementedException();
            }
        }
    }
}
