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