using FamilyHubs.Referral.Core.ClientServices;
using FamilyHubs.Referral.Core.Commands.CreateReferral;
using FamilyHubs.Referral.Core.Commands.SetReferralStatus;
using FamilyHubs.Referral.Core.Commands.UpdateReferral;
using FamilyHubs.Referral.Core.Queries.GetReferrals;
using FamilyHubs.Referral.Core.Queries.GetReferralStatus;
using FamilyHubs.Referral.Data.Entities;
using FamilyHubs.Referral.Data.Repository;
using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.ReferralService.Shared.Dto.CreateUpdate;
using FamilyHubs.ReferralService.Shared.Dto.Metrics;
using FamilyHubs.ReferralService.Shared.Enums;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Identity.Models;
using FluentAssertions;
using NSubstitute;

namespace FamilyHubs.Referral.UnitTests;

public class WhenUsingReferralCommands : BaseCreateDbUnitTest<CreateReferralCommandHandler>
{
    private const long ExpectedAccountId = 123L;
    private const long ExpectedOrganisationId = 456L;

    private readonly IServiceDirectoryService _serviceDirectoryService;
    private DateTimeOffset RequestTimestamp { get; }
    private FamilyHubsUser FamilyHubsUser { get; }

    public WhenUsingReferralCommands()
    {
        RequestTimestamp = new DateTimeOffset(new DateTime(2025, 1, 1, 12, 0, 0));

        FamilyHubsUser = new FamilyHubsUser
        {
            AccountId = ExpectedAccountId.ToString(),
            OrganisationId = ExpectedOrganisationId.ToString()
        };

        _serviceDirectoryService = Substitute.For<IServiceDirectoryService>();

        _serviceDirectoryService.GetOrganisationById(Arg.Any<long>())!
            .Returns(Task.FromResult(new ServiceDirectory.Shared.Dto.OrganisationDto
            {
                Id = 2,
                Name = "Organisation",
                Description = "Organisation Description",
                OrganisationType = ServiceDirectory.Shared.Enums.OrganisationType.VCFS,
                AdminAreaCode = "AdminAreaCode"
            }));

        _serviceDirectoryService.GetServiceById(Arg.Any<long>())!
            .Returns(Task.FromResult(new ServiceDirectory.Shared.Dto.ServiceDto
            {
                Id = 2,
                Name = "Service",
                Description = "Service Description",
                ServiceType = ServiceDirectory.Shared.Enums.ServiceType.FamilyExperience
            }));
    }

    // TODO: Can probably remove this thing
#if UseJsonService
        const string JsonService = "{\"Id\":\"ba1cca90-b02a-4a0b-afa0-d8aed1083c0d\",\"Name\":\"Test County Council\",\"Description\":\"Test County Council\",\"Logo\":null,\"Uri\":\"https://www.test.gov.uk/\",\"Url\":\"https://www.test.gov.uk/\",\"Services\":[{\"Id\":\"c1b5dd80-7506-4424-9711-fe175fa13eb8\",\"Name\":\"Test Organisation for Children with Tracheostomies\",\"Description\":\"Test Organisation for for Children with Tracheostomies is a national self help group operating as a registered charity and is run by parents of children with a tracheostomy and by people who sympathise with the needs of such families. ACT as an organisation is non profit making, it links groups and individual members throughout Great Britain and Northern Ireland.\",\"Accreditations\":null,\"Assured_date\":null,\"Attending_access\":null,\"Attending_type\":null,\"Deliverable_type\":null,\"Status\":\"active\",\"Url\":\"www.testservice.com\",\"Email\":\"support@testservice.com\",\"Fees\":null,\"ServiceDelivery\":[{\"Id\":\"14db2aef-9292-4afc-be09-5f6f43765938\",\"ServiceDelivery\":2}],\"Eligibilities\":[{\"Id\":\"Test9109Children\",\"Eligibility\":\"\",\"Maximum_age\":0,\"Minimum_age\":13}],\"Contacts\":[{\"Id\":\"5eac5cb6-cc7e-444d-a29b-76ccb85be866\",\"Title\":\"Service\",\"Name\":\"\",\"Phones\":[{\"Id\":\"1568\",\"Number\":\"01827 65779\"}]}],\"Cost_options\":[],\"Languages\":[{\"Id\":\"442a06cd-aa14-4ea3-9f11-b45c1bc4861f\",\"Language\":\"English\"}],\"Service_areas\":[{\"Id\":\"68af19cd-bc81-4585-99a2-85a2b0d62a1d\",\"Service_area\":\"National\",\"Extent\":null,\"Uri\":\"http://statistics.data.gov.uk/id/statistical-geography/K02000001\"}],\"Service_at_locations\":[{\"Id\":\"Test1749\",\"Location\":{\"Id\":\"a878aadc-6097-4a0f-b3e1-77fd4511175d\",\"Name\":\"\",\"Description\":\"\",\"Latitude\":52.6312,\"Longitude\":-1.66526,\"Physical_addresses\":[{\"Id\":\"1076aaa8-f99d-4395-8e4f-c0dde8095085\",\"Address_1\":\"75 Sheepcote Lane\",\"City\":\", Stathe, Tamworth, Staffordshire, \",\"Postal_code\":\"B77 3JN\",\"Country\":\"England\",\"State_province\":null}]}}],\"Service_taxonomys\":[{\"Id\":\"Test9107\",\"Taxonomy\":{\"Id\":\"Test bccsource:Organisation\",\"Name\":\"Organisation\",\"Vocabulary\":\"Test BCC Data Sources\",\"Parent\":null}},{\"Id\":\"Test9108\",\"Taxonomy\":{\"Id\":\"Test bccprimaryservicetype:38\",\"Name\":\"Support\",\"Vocabulary\":\"Test BCC Primary Services\",\"Parent\":null}},{\"Id\":\"Test9109\",\"Taxonomy\":{\"Id\":\"Test bccagegroup:37\",\"Name\":\"Children\",\"Vocabulary\":\"Test BCC Age Groups\",\"Parent\":null}},{\"Id\":\"Test9110\",\"Taxonomy\":{\"Id\":\"Testbccusergroup:56\",\"Name\":\"Long Term Health Conditions\",\"Vocabulary\":\"Test BCC User Groups\",\"Parent\":null}}]}]}";
#endif

    [Fact]
    public async Task ThenCreateReferral()
    {
        //Arrange
        MockApplicationDbContext.Statuses.AddRange(ReferralSeedData.SeedStatuses());
        MockApplicationDbContext.Roles.AddRange(ReferralSeedData.SeedRoles());
        await MockApplicationDbContext.SaveChangesAsync();

        var testReferral = GetReferralDto();
        testReferral.Status.Id = 0;
        var createReferral = new CreateReferralDto(testReferral, new ConnectionRequestsSentMetricDto(RequestTimestamp));

        CreateReferralCommand command = new(createReferral, FamilyHubsUser);
        CreateReferralCommandHandler handler = new(MockApplicationDbContext, Mapper, _serviceDirectoryService);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Id.Should().BeGreaterThan(0);
        result.Id.Should().Be(testReferral.Id);
        result.ServiceName.Should().Be(testReferral.ReferralServiceDto.Name);
    }

    [Fact]
    public async Task ThenCreateReferralWithOrganisationNullReturnFromServiceDirectoryService()
    {
        // Arrange
        MockApplicationDbContext.Statuses.AddRange(ReferralSeedData.SeedStatuses());
        MockApplicationDbContext.Roles.AddRange(ReferralSeedData.SeedRoles());

        await MockApplicationDbContext.SaveChangesAsync();

        ServiceDirectory.Shared.Dto.OrganisationDto? organisation = null;
        _serviceDirectoryService.GetOrganisationById(Arg.Any<long>()).Returns(Task.FromResult(organisation));

        var testReferral = GetReferralDto();
        testReferral.Status.Id = 0;
        var createReferral = new CreateReferralDto(testReferral, new ConnectionRequestsSentMetricDto(RequestTimestamp));

        CreateReferralCommand command = new(createReferral, FamilyHubsUser);
        CreateReferralCommandHandler handler = new(MockApplicationDbContext, Mapper, _serviceDirectoryService);

        //Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Failed to return Organisation from service directory for Id = 0");
    }

    [Fact]
    public async Task ThenCreateReferralWithServiceNullReturnFromServiceDirectoryService()
    {
        //Arrange
        MockApplicationDbContext.Statuses.AddRange(ReferralSeedData.SeedStatuses());
        MockApplicationDbContext.Roles.AddRange(ReferralSeedData.SeedRoles());

        await MockApplicationDbContext.SaveChangesAsync();

        ServiceDirectory.Shared.Dto.ServiceDto? service = null;
        _serviceDirectoryService.GetServiceById(Arg.Any<long>()).Returns(Task.FromResult(service));

        var testReferral = GetReferralDto();
        testReferral.Status.Id = 0;
        var createReferral = new CreateReferralDto(testReferral, new ConnectionRequestsSentMetricDto(RequestTimestamp));
        CreateReferralCommand command = new(createReferral, FamilyHubsUser);
        CreateReferralCommandHandler handler = new(MockApplicationDbContext, Mapper, _serviceDirectoryService);

        //Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        //Assert
        await act.Should().ThrowAsync<ArgumentException>().WithMessage("Failed to return Service from service directory for Id = 2");
    }

    [Fact]
    public async Task ThenCreateReferralWithExitingReferrer()
    {
        //Arrange
        MockApplicationDbContext.Referrals.Add(ReferralSeedData.SeedReferral().ElementAt(0));

        await MockApplicationDbContext.SaveChangesAsync();

        var testReferral = GetReferralDto();
        testReferral.ReferralUserAccountDto = Mapper.Map<UserAccountDto>(ReferralSeedData.SeedReferral().ElementAt(0).UserAccount);
        testReferral.ReferralUserAccountDto.Id = MockApplicationDbContext.UserAccounts.First().Id;
        var createReferral = new CreateReferralDto(testReferral, new ConnectionRequestsSentMetricDto(RequestTimestamp));

        CreateReferralCommand command = new(createReferral, FamilyHubsUser);
        CreateReferralCommandHandler handler = new(MockApplicationDbContext, Mapper, _serviceDirectoryService);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Id.Should().BeGreaterThan(0);
        result.Id.Should().Be(testReferral.Id);
        result.ServiceName.Should().Be(testReferral.ReferralServiceDto.Name);
    }

    [Fact]
    public async Task ThenCreateReferralWithExitingRecipientEmail()
    {
        //Arrange
        MockApplicationDbContext.Statuses.AddRange(ReferralSeedData.SeedStatuses());
        MockApplicationDbContext.Roles.AddRange(ReferralSeedData.SeedRoles());

        await MockApplicationDbContext.SaveChangesAsync();

        var referral = ReferralSeedData.SeedReferral().ElementAt(0);
        var status = MockApplicationDbContext.Statuses.SingleOrDefault(x => x.Name == referral.Status.Name);
        if (status is not null)
        {
            referral.Status = status;
        }

        MockApplicationDbContext.Referrals.Add(referral);

        await MockApplicationDbContext.SaveChangesAsync();

        var testReferral = GetReferralDto();
        testReferral.RecipientDto = new RecipientDto
        {
            Id = MockApplicationDbContext.Recipients.First().Id,
            Name = ReferralSeedData.SeedReferral().ElementAt(0).Recipient.Name,
            Email = ReferralSeedData.SeedReferral().ElementAt(0).Recipient.Email,
        };
        var createReferral = new CreateReferralDto(testReferral, new ConnectionRequestsSentMetricDto(RequestTimestamp));

        CreateReferralCommand command = new(createReferral, FamilyHubsUser);
        CreateReferralCommandHandler handler = new(MockApplicationDbContext, Mapper, _serviceDirectoryService);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Id.Should().BeGreaterThan(0);
        result.Id.Should().Be(testReferral.Id);
        result.ServiceName.Should().Be(testReferral.ReferralServiceDto.Name);
    }

    [Fact]
    public async Task ThenCreateReferralWithExitingRecipientTelephone()
    {
        //Arrange
        MockApplicationDbContext.Statuses.AddRange(ReferralSeedData.SeedStatuses());
        MockApplicationDbContext.Roles.AddRange(ReferralSeedData.SeedRoles());

        await MockApplicationDbContext.SaveChangesAsync();

        var referral = ReferralSeedData.SeedReferral().ElementAt(0);
        var status = MockApplicationDbContext.Statuses.SingleOrDefault(x => x.Name == referral.Status.Name);
        if (status is not null)
        {
            referral.Status = status;
        }

        MockApplicationDbContext.Referrals.Add(referral);

        await MockApplicationDbContext.SaveChangesAsync();

        var testReferral = GetReferralDto();
        testReferral.RecipientDto = new RecipientDto
        {
            Id = MockApplicationDbContext.Recipients.First().Id,
            Name = ReferralSeedData.SeedReferral().ElementAt(0).Recipient.Name,
            Telephone = ReferralSeedData.SeedReferral().ElementAt(0).Recipient.Telephone,
        };
        var createReferral = new CreateReferralDto(testReferral, new ConnectionRequestsSentMetricDto(RequestTimestamp));

        CreateReferralCommand command = new(createReferral, FamilyHubsUser);
        CreateReferralCommandHandler handler = new(MockApplicationDbContext, Mapper, _serviceDirectoryService);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Id.Should().BeGreaterThan(0);
        result.Id.Should().Be(testReferral.Id);
        result.ServiceName.Should().Be(testReferral.ReferralServiceDto.Name);
    }

    [Fact]
    public async Task ThenCreateReferralWithExitingRecipientTextphone()
    {
        //Arrange
        MockApplicationDbContext.Statuses.AddRange(ReferralSeedData.SeedStatuses());
        MockApplicationDbContext.Roles.AddRange(ReferralSeedData.SeedRoles());

        await MockApplicationDbContext.SaveChangesAsync();

        var referral = ReferralSeedData.SeedReferral().ElementAt(0);
        var status = MockApplicationDbContext.Statuses.SingleOrDefault(x => x.Name == referral.Status.Name);
        if (status is not null)
        {
            referral.Status = status;
        }

        MockApplicationDbContext.Referrals.Add(referral);

        await MockApplicationDbContext.SaveChangesAsync();

        var testReferral = GetReferralDto();
        testReferral.RecipientDto = new RecipientDto
        {
            Id = MockApplicationDbContext.Recipients.First().Id,
            Name = ReferralSeedData.SeedReferral().ElementAt(0).Recipient.Name,
            TextPhone = ReferralSeedData.SeedReferral().ElementAt(0).Recipient.TextPhone,
        };
        var createReferral = new CreateReferralDto(testReferral, new ConnectionRequestsSentMetricDto(RequestTimestamp));

        CreateReferralCommand command = new(createReferral, FamilyHubsUser);
        CreateReferralCommandHandler handler = new(MockApplicationDbContext, Mapper, _serviceDirectoryService);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Id.Should().BeGreaterThan(0);
        result.Id.Should().Be(testReferral.Id);
        result.ServiceName.Should().Be(testReferral.ReferralServiceDto.Name);
    }

    [Fact]
    public async Task ThenCreateReferralWithExitingRecipientNameAndPostCode()
    {
        //Arrange
        MockApplicationDbContext.Statuses.AddRange(ReferralSeedData.SeedStatuses());
        MockApplicationDbContext.Roles.AddRange(ReferralSeedData.SeedRoles());

        await MockApplicationDbContext.SaveChangesAsync();

        var referral = ReferralSeedData.SeedReferral().ElementAt(0);
        var status = MockApplicationDbContext.Statuses.SingleOrDefault(x => x.Name == referral.Status.Name);
        if (status is not null)
        {
            referral.Status = status;
        }
        MockApplicationDbContext.Referrals.Add(referral);

        await MockApplicationDbContext.SaveChangesAsync();

        var testReferral = GetReferralDto();
        testReferral.RecipientDto = new RecipientDto
        {
            Id = MockApplicationDbContext.Recipients.First().Id,
            Name = ReferralSeedData.SeedReferral().ElementAt(0).Recipient.Name,
            PostCode = ReferralSeedData.SeedReferral().ElementAt(0).Recipient.PostCode,
        };
        var createReferral = new CreateReferralDto(testReferral, new ConnectionRequestsSentMetricDto(RequestTimestamp));

        CreateReferralCommand command = new(createReferral, FamilyHubsUser);
        CreateReferralCommandHandler handler = new(MockApplicationDbContext, Mapper, _serviceDirectoryService);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Id.Should().BeGreaterThan(0);
        result.Id.Should().Be(testReferral.Id);
        result.ServiceName.Should().Be(testReferral.ReferralServiceDto.Name);
    }

    [Fact]
    public async Task ThenCreateReferralWithExitingServiceAndUpdateSubProporties()
    {
        //Arrange
        MockApplicationDbContext.Statuses.AddRange(ReferralSeedData.SeedStatuses());
        MockApplicationDbContext.Roles.AddRange(ReferralSeedData.SeedRoles());

        await MockApplicationDbContext.SaveChangesAsync();

        var referral = ReferralSeedData.SeedReferral().ElementAt(0);
        var status = MockApplicationDbContext.Statuses.SingleOrDefault(x => x.Name == referral.Status.Name);
        if (status is not null)
        {
            referral.Status = status;
        }

        MockApplicationDbContext.Referrals.Add(referral);

        await MockApplicationDbContext.SaveChangesAsync();

        var testReferral = GetReferralDto();
        testReferral.ReferralServiceDto = Mapper.Map<ReferralServiceDto>(ReferralSeedData.SeedReferral().ElementAt(0).ReferralService);

        testReferral.ReasonForSupport = "New Reason For Support";
        testReferral.EngageWithFamily = "New Engage With Family";
        testReferral.RecipientDto.Telephone = "078123459";
        testReferral.RecipientDto.TextPhone = "078123459";
        testReferral.RecipientDto.AddressLine1 = "Address Line 1A";
        testReferral.RecipientDto.AddressLine2 = "Address Line 2A";
        testReferral.RecipientDto.TownOrCity = "Town or CityA";
        testReferral.RecipientDto.County = "CountyA";
        testReferral.ReferralUserAccountDto.PhoneNumber = "1234567899";
        testReferral.ReferralServiceDto.Id = 0;
        testReferral.ReferralServiceDto.Name = "Service A";
        testReferral.ReferralServiceDto.Description = "Service Description A";
        testReferral.ReferralServiceDto.OrganisationDto.Id = 0;
        testReferral.ReferralServiceDto.OrganisationDto.Name = "Organisation A";
        testReferral.ReferralServiceDto.OrganisationDto.Description = "Organisation Description A";

        var createReferral = new CreateReferralDto(testReferral, new ConnectionRequestsSentMetricDto(RequestTimestamp));
        CreateReferralCommand command = new(createReferral, FamilyHubsUser);
        CreateReferralCommandHandler handler = new(MockApplicationDbContext, Mapper, _serviceDirectoryService);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Id.Should().BeGreaterThan(0);
        result.Id.Should().Be(testReferral.Id);
        result.ServiceName.Should().Be(testReferral.ReferralServiceDto.Name);

        GetReferralByIdCommand getCommand = new(result.Id);
        GetReferralByIdCommandHandler getHandler = new(MockApplicationDbContext, Mapper);


        //Check and Assert
        var getResult = await getHandler.Handle(getCommand, new CancellationToken());

        testReferral.ReferralServiceDto.Id = 0;
        testReferral.ReferralServiceDto.OrganisationDto.ReferralServiceId = 0;
        testReferral.Status.SecondrySortOrder = 1;
        getResult.Should().BeEquivalentTo(testReferral, options => options.Excluding(x => x.Created).Excluding(x => x.LastModified).Excluding(x => x.ReferralUserAccountDto.UserAccountRoles));


    }

    [Fact]
    public async Task ThenUpdateReferral()
    {
        // Arrange
        MockApplicationDbContext.Statuses.AddRange(ReferralSeedData.SeedStatuses());
        MockApplicationDbContext.Roles.AddRange(ReferralSeedData.SeedRoles());

        await MockApplicationDbContext.SaveChangesAsync();

        var testReferral = GetReferralDto();
        var createReferral = new CreateReferralDto(testReferral, new ConnectionRequestsSentMetricDto(RequestTimestamp));

        CreateReferralCommand createCommand = new(createReferral, FamilyHubsUser);
        CreateReferralCommandHandler createHandler = new(MockApplicationDbContext, Mapper, _serviceDirectoryService);

        var setResult = await createHandler.Handle(createCommand, default);

        testReferral.RecipientDto.Name += " Test";
        testReferral.RecipientDto.Email += " Test";
        testReferral.RecipientDto.Telephone += " Test";
        testReferral.RecipientDto.TextPhone += " Test";
        testReferral.ReasonForSupport += " Test";

        UpdateReferralCommand command = new(setResult.Id, testReferral);
        UpdateReferralCommandHandler handler = new(MockApplicationDbContext, Mapper);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        setResult.Id.Should().BeGreaterThan(0);
        setResult.Id.Should().Be(testReferral.Id);
        setResult.ServiceName.Should().Be(testReferral.ReferralServiceDto.Name);
        result.Should().BeGreaterThan(0);
        result.Should().Be(testReferral.Id);
    }

    [Theory]
    [InlineData(null, null, 1)]
    [InlineData(ReferralOrderBy.NotSet, true, 1)]
    [InlineData(ReferralOrderBy.DateSent, true, 1)]
    [InlineData(ReferralOrderBy.DateSent, false, 2)]
    [InlineData(ReferralOrderBy.DateUpdated, true, 1)]
    [InlineData(ReferralOrderBy.DateUpdated, false, 2)]
    [InlineData(ReferralOrderBy.Status, true, 2)] //--
    [InlineData(ReferralOrderBy.Status, false, 2)]
    [InlineData(ReferralOrderBy.RecipientName, true, 1)]
    [InlineData(ReferralOrderBy.RecipientName, false, 2)]
    [InlineData(ReferralOrderBy.Team, true, 1)]
    [InlineData(ReferralOrderBy.Team, false, 1)]
    [InlineData(ReferralOrderBy.ServiceName, true, 1)]
    [InlineData(ReferralOrderBy.ServiceName, false, 1)]
    public async Task ThenGetReferralsByReferrer(ReferralOrderBy? referralOrderBy, bool? isAssending, int firstId)
    {
        //Arrange
        await CreateReferrals(MockApplicationDbContext);

        GetReferralsByReferrerCommand getCommand = new("Joe.Professional@email.com", referralOrderBy, isAssending, null, 1, 10);
        GetReferralsByReferrerCommandHandler getHandler = new(MockApplicationDbContext, Mapper);

        //Act
        var result = await getHandler.Handle(getCommand, new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Items.Count.Should().Be(2);
        result.Items[0].Created.Should().NotBeNull();
        result.Items[0].Id.Should().Be(firstId);
    }

    [Theory]
    [InlineData(null, null, 1)]
    [InlineData(ReferralOrderBy.NotSet, true, 1)]
    [InlineData(ReferralOrderBy.DateSent, true, 1)]
    [InlineData(ReferralOrderBy.DateSent, false, 2)]
    [InlineData(ReferralOrderBy.DateUpdated, true, 1)]
    [InlineData(ReferralOrderBy.DateUpdated, false, 2)]
    [InlineData(ReferralOrderBy.Status, true, 2)]
    [InlineData(ReferralOrderBy.Status, false, 2)]
    [InlineData(ReferralOrderBy.RecipientName, true, 1)]
    [InlineData(ReferralOrderBy.RecipientName, false, 2)]
    [InlineData(ReferralOrderBy.Team, true, 1)]
    [InlineData(ReferralOrderBy.Team, false, 1)]
    [InlineData(ReferralOrderBy.ServiceName, true, 1)]
    [InlineData(ReferralOrderBy.ServiceName, false, 1)]
    public async Task ThenGetReferralsByReferrerId(ReferralOrderBy? referralOrderBy, bool? isAscending, int firstId)
    {
        //Arrange
        await CreateReferrals(MockApplicationDbContext);

        GetReferralsByReferrerByReferrerIdCommand getCommand = new(5, referralOrderBy, isAscending, false, 1, 10);
        GetReferralsByReferrerByReferrerIdCommandHandler getHandler = new(MockApplicationDbContext, Mapper);

        //Act
        var result = await getHandler.Handle(getCommand, default);

        //Assert
        result.Should().NotBeNull();
        result.Items.Count.Should().Be(2);
        result.Items[0].Created.Should().NotBeNull();
        result.Items[0].Id.Should().Be(firstId);
    }

    [Theory]
    [InlineData(ReferralOrderBy.DateSent, true, 1)]
    [InlineData(ReferralOrderBy.DateSent, false, 2)]
    [InlineData(ReferralOrderBy.DateUpdated, true, 1)]
    [InlineData(ReferralOrderBy.DateUpdated, false, 2)]
    [InlineData(ReferralOrderBy.Status, true, 1)]
    [InlineData(ReferralOrderBy.Status, false, 2)]
    [InlineData(ReferralOrderBy.RecipientName, true, 1)]
    [InlineData(ReferralOrderBy.RecipientName, false, 2)]
    [InlineData(ReferralOrderBy.Team, true, 1)]
    [InlineData(ReferralOrderBy.Team, false, 1)]
    [InlineData(ReferralOrderBy.ServiceName, true, 1)]
    [InlineData(ReferralOrderBy.ServiceName, false, 1)]
    public async Task ThenGetReferralsByOrganisationId(ReferralOrderBy referralOrderBy, bool isAscending, int firstId)
    {
        // Arrange
        await CreateReferrals(MockApplicationDbContext);

        GetReferralsByOrganisationIdCommand getCommand = new(1, referralOrderBy, isAscending, null, 1, 10);
        GetReferralsByOrganisationIdCommandHandler getHandler = new(MockApplicationDbContext, Mapper);

        //Act
        var result = await getHandler.Handle(getCommand, new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Items.Count.Should().Be(2);
        result.Items[0].Created.Should().NotBeNull();
        result.Items[0].Id.Should().Be(firstId);
    }

    [Fact]
    public async Task ThenGetReferralsById()
    {
        // Arrange
        var testReferral = GetReferralDto();
        testReferral.ReasonForDecliningSupport = "Reason For Declining Support";
        var createReferral = new CreateReferralDto(testReferral, new ConnectionRequestsSentMetricDto(RequestTimestamp));

        CreateReferralCommand command = new(createReferral, FamilyHubsUser);
        CreateReferralCommandHandler handler = new(MockApplicationDbContext, Mapper, _serviceDirectoryService);

        var response = await handler.Handle(command, default);

        GetReferralByIdCommand getCommand = new(response.Id);
        GetReferralByIdCommandHandler getHandler = new(MockApplicationDbContext, Mapper);

        //Act
        var result = await getHandler.Handle(getCommand, default);

        //Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(response.Id);
        result.Created.Should().NotBeNull();
        result.ReasonForDecliningSupport.Should().Be("Reason For Declining Support");
    }

    [Fact]
    public async Task ThenGetReferralStatusList()
    {
        // Arrange
        IReadOnlyCollection<Status> statuses = ReferralSeedData.SeedStatuses();
        MockApplicationDbContext.Statuses.AddRange(statuses);
        MockApplicationDbContext.Roles.AddRange(ReferralSeedData.SeedRoles());

        await MockApplicationDbContext.SaveChangesAsync();

        GetReferralStatusesCommand command = new();
        GetReferralStatusesCommandHandler handler = new(MockApplicationDbContext, Mapper);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Count.Should().Be(statuses.Count);
    }

    [Theory]
    [InlineData(null, null, null, null)]
    [InlineData(null, 1L, null, null)]
    [InlineData(null, null, 1L, null)]
    [InlineData(null, null, null, 2L)]
    [InlineData(2L, 1L, 1L, 2L)]
    public async Task ThenGetReferralsByCompositeKey(long? serviceId, long? statusId, long? recipientId, long? referralId)
    {
        // Arrange
        var testReferral = GetReferralDto();
        var createReferral = new CreateReferralDto(testReferral, new ConnectionRequestsSentMetricDto(RequestTimestamp));

        CreateReferralCommand command = new(createReferral, FamilyHubsUser);
        CreateReferralCommandHandler handler = new(MockApplicationDbContext, Mapper, _serviceDirectoryService);

        await handler.Handle(command, default);

        GetReferralByServiceIdStatusIdRecipientIdReferrerIdCommand getCommand = new(serviceId, statusId, recipientId, referralId, ReferralOrderBy.RecipientName, true, null, 1, 10);
        GetReferralByServiceIdStatusIdRecipientIdReferrerIdCommandHandler getHandler = new(MockApplicationDbContext, Mapper);

        //Act
        var result = await getHandler.Handle(getCommand, new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        result.Items.Count.Should().Be(1);
    }

    [Theory]
    [InlineData(RoleTypes.DfeAdmin)]
    [InlineData(RoleTypes.VcsProfessional)]
    [InlineData(RoleTypes.VcsDualRole)]
    public async Task ThenUpdateStatusOfReferral(string role)
    {
        //Arrange
        MockApplicationDbContext.Statuses.AddRange(ReferralSeedData.SeedStatuses());
        MockApplicationDbContext.Roles.AddRange(ReferralSeedData.SeedRoles());
        await MockApplicationDbContext.SaveChangesAsync();

        var testReferral = GetReferralDto();
        var createReferral = new CreateReferralDto(testReferral, new ConnectionRequestsSentMetricDto(RequestTimestamp));

        CreateReferralCommand createCommand = new(createReferral, FamilyHubsUser);
        CreateReferralCommandHandler createHandler = new(MockApplicationDbContext, Mapper, _serviceDirectoryService);

        var setupResult = await createHandler.Handle(createCommand, default);

        SetReferralStatusCommand command = new(role, testReferral.ReferralServiceDto.OrganisationDto.Id, testReferral.Id, "Declined", "Unable to help");
        SetReferralStatusCommandHandler handler = new(MockApplicationDbContext);

        //Act
        var result = await handler.Handle(command, default);

        //Assert
        setupResult.Id.Should().BeGreaterThan(0);
        setupResult.Id.Should().Be(testReferral.Id);
        result.Should().NotBeNull();
        result.Should().Be("Declined");
    }

    [Fact]
    public async Task ThenUpdateStatusOfReferralReturnsForbidden()
    {
        // Arrange
        MockApplicationDbContext.Statuses.AddRange(ReferralSeedData.SeedStatuses());
        MockApplicationDbContext.Roles.AddRange(ReferralSeedData.SeedRoles());

        await MockApplicationDbContext.SaveChangesAsync();

        var testReferral = GetReferralDto();
        var createReferral = new CreateReferralDto(testReferral, new ConnectionRequestsSentMetricDto(RequestTimestamp));

        CreateReferralCommand createCommand = new(createReferral, FamilyHubsUser);
        CreateReferralCommandHandler createHandler = new(MockApplicationDbContext, Mapper, _serviceDirectoryService);

        var setupResult = await createHandler.Handle(createCommand, default);

        SetReferralStatusCommand command = new(RoleTypes.LaProfessional, -1, testReferral.Id, "Declined", "Unable to help");
        SetReferralStatusCommandHandler handler = new(MockApplicationDbContext);

        //Act
        var result = await handler.Handle(command, default);

        //Assert
        setupResult.Id.Should().BeGreaterThan(0);
        setupResult.Id.Should().Be(testReferral.Id);
        result.Should().NotBeNull();
        result.Should().Be(SetReferralStatusCommandHandler.Forbidden);
    }
}