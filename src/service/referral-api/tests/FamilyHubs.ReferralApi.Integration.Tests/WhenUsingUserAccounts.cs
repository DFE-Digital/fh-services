﻿using FamilyHubs.Referral.Core.Commands.CreateUserAccount;
using FamilyHubs.Referral.Core.Commands.UpdateUserAccount;
using FamilyHubs.Referral.Core.Queries.GetUserAccounts;
using FamilyHubs.Referral.Data.Entities;
using FamilyHubs.Referral.Data.Repository;
using FamilyHubs.ReferralService.Shared.Dto;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace FamilyHubs.Referral.Integration.Tests;

[Collection("Sequential")]
public class WhenUsingUserAccounts : DataIntegrationTestBase
{
    [Fact]
    public async Task ThenCreateASingleUserAccount()
    {
#pragma warning disable CS8602
        //Assign 
        UserAccountDto userAccountDto = TestDataProvider.GetUserAccount();
        UserAccount userAccount = Mapper.Map<UserAccount>(userAccountDto);
        userAccount.OrganisationUserAccounts = Mapper.Map<List<UserAccountOrganisation>>(userAccountDto.OrganisationUserAccounts);

        CreateUserAccountCommand command = new CreateUserAccountCommand(userAccountDto);
        CreateUserAccountCommandHandler handler = new CreateUserAccountCommandHandler(TestDbContext, Mapper);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().BeGreaterThan(0);
        var actualUserAccount = await TestDbContext.UserAccounts
            .Include(x => x.OrganisationUserAccounts)
            .FirstAsync(x => x.OrganisationUserAccounts != null && x.OrganisationUserAccounts.Any(uao => uao.OrganisationId == userAccountDto.OrganisationUserAccounts[0].Organisation.Id));

        actualUserAccount.EmailAddress.Should().Be(userAccount.EmailAddress);
        actualUserAccount.PhoneNumber.Should().Be(userAccount.PhoneNumber);
        actualUserAccount.OrganisationUserAccounts[0].OrganisationId.Should().Be(userAccountDto.OrganisationUserAccounts[0].Organisation.Id);


#pragma warning restore CS8602
    }

    [Fact]
    public async Task ThenCreateASingleUserAccountWithoutOrganisation()
    {
#pragma warning disable CS8602
        //Assign 
        UserAccountDto userAccountDto = new UserAccountDto
        {
            Id = 3,
            EmailAddress = "SecondUser@email.com",
            Name = "Second User",
            PhoneNumber = "0161 111 3333",
            Team = "Test Team"
            
        };

        var userAccount = Mapper.Map<UserAccount>(userAccountDto);
        userAccount.OrganisationUserAccounts = Mapper.Map<List<UserAccountOrganisation>>(userAccountDto.OrganisationUserAccounts);

        CreateUserAccountCommand command = new CreateUserAccountCommand(userAccountDto);
        CreateUserAccountCommandHandler handler = new CreateUserAccountCommandHandler(TestDbContext, Mapper);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().BeGreaterThan(0);
        var actualUserAccount = await TestDbContext.UserAccounts
            .Include(x => x.OrganisationUserAccounts)
            .FirstAsync(x => x.Id == userAccountDto.Id);

        actualUserAccount.EmailAddress.Should().Be(userAccount.EmailAddress);
        actualUserAccount.PhoneNumber.Should().Be(userAccount.PhoneNumber);
        
#pragma warning restore CS8602
    }

    [Fact]
    public async Task ThenCreateASingleUserAccountWithOrganisationWithOnlyId()
    {
#pragma warning disable CS8602
        //Assign 
        UserAccountDto userAccountDto = new UserAccountDto
        {
            Id = 3,
            EmailAddress = "SecondUser@email.com",
            Name = "Second User",
            PhoneNumber = "0161 111 3333",
            Team = "Test Team",
            OrganisationUserAccounts = new List<UserAccountOrganisationDto>
            {
                new()
                {
                    UserAccount = new UserAccountDto
                    {
                        Id = 3,
                        EmailAddress = "SecondUser@email.com",
                        Name = "Second User",
                        PhoneNumber = "0161 111 3333",
                        Team = "Test Team"
                    },
                    Organisation = new OrganisationDto
                    {
                        Id = 9999,
                        Name = "",
                        Description = "",
                    }
                }
            }
        };

        var userAccount = Mapper.Map<UserAccount>(userAccountDto);
        userAccount.OrganisationUserAccounts = Mapper.Map<List<UserAccountOrganisation>>(userAccountDto.OrganisationUserAccounts);

        CreateUserAccountCommand command = new CreateUserAccountCommand(userAccountDto);
        CreateUserAccountCommandHandler handler = new CreateUserAccountCommandHandler(TestDbContext, Mapper);

        //Act
        var result = await handler.Handle(command, default);

        //Assert
        result.Should().BeGreaterThan(0);
        var actualUserAccount = await TestDbContext.UserAccounts
            .Include(x => x.OrganisationUserAccounts)
            .FirstAsync(x => x.Id == userAccountDto.Id);

        actualUserAccount.EmailAddress.Should().Be(userAccount.EmailAddress);
        actualUserAccount.PhoneNumber.Should().Be(userAccount.PhoneNumber);

#pragma warning restore CS8602
    }

    [Fact]
    public async Task ThenCreateUserAccounts()
    {
#pragma warning disable CS8602
        //Assign 
        UserAccountDto userAccountDto = TestDataProvider.GetUserAccount();
        List<UserAccountDto> listUserAccounts = [userAccountDto];
        UserAccount userAccount = Mapper.Map<UserAccount>(userAccountDto);
        userAccount.OrganisationUserAccounts = Mapper.Map<List<UserAccountOrganisation>>(userAccountDto.OrganisationUserAccounts);

        CreateUserAccountsCommand command = new CreateUserAccountsCommand(listUserAccounts);
        CreateUserAccountsCommandHandler handler = new CreateUserAccountsCommandHandler(TestDbContext, Mapper);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().BeTrue();
        var actualUserAccount = await TestDbContext.UserAccounts
            .Include(x => x.OrganisationUserAccounts)
            .FirstAsync(x => x.OrganisationUserAccounts != null && x.OrganisationUserAccounts.Any(uao => uao.OrganisationId == userAccountDto.OrganisationUserAccounts[0].Organisation.Id));

        actualUserAccount.EmailAddress.Should().Be(userAccount.EmailAddress);
        actualUserAccount.PhoneNumber.Should().Be(userAccount.PhoneNumber);
        actualUserAccount.OrganisationUserAccounts[0].OrganisationId.Should().Be(userAccountDto.OrganisationUserAccounts[0].Organisation.Id);
        

#pragma warning restore CS8602
    }

    [Fact]
    public async Task ThenUpdateASingleUserAccount()
    {
#pragma warning disable CS8602
        // Arrange

        UserAccountDto userAccountDto = TestDataProvider.GetUserAccount();

        var userAccount = Mapper.Map<UserAccount>(userAccountDto);
        userAccount.OrganisationUserAccounts = Mapper.Map<List<UserAccountOrganisation>>(userAccountDto.OrganisationUserAccounts);
        TestDbContext.Organisations.Add(userAccount.OrganisationUserAccounts[0].Organisation);
        await TestDbContext.SaveChangesAsync();
        userAccount.OrganisationUserAccounts[0].Organisation = TestDbContext.Organisations.First(x => x.Id == userAccount.OrganisationUserAccounts[0].Organisation.Id);
        TestDbContext.UserAccounts.Add(userAccount);
        await TestDbContext.SaveChangesAsync();
        userAccountDto.Id = userAccount.Id;
        userAccountDto.EmailAddress = "UpdatedUser@email.com";
        userAccountDto.PhoneNumber = "0161 111 1112";

        UpdateUserAccountCommand command = new UpdateUserAccountCommand(userAccount.Id,userAccountDto);
        UpdateUserAccountCommandHandler handler = new UpdateUserAccountCommandHandler(TestDbContext, Mapper);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().BeTrue();
        var actualUserAccount = await TestDbContext.UserAccounts
            .Include(x => x.OrganisationUserAccounts)
            .FirstAsync(x => x.OrganisationUserAccounts != null && x.OrganisationUserAccounts.Any(uao => uao.OrganisationId == userAccountDto.OrganisationUserAccounts[0].Organisation.Id));

        actualUserAccount.EmailAddress.Should().Be(userAccount.EmailAddress);
        actualUserAccount.PhoneNumber.Should().Be(userAccount.PhoneNumber);
        actualUserAccount.OrganisationUserAccounts[0].OrganisationId.Should().Be(userAccountDto.OrganisationUserAccounts[0].Organisation.Id);


#pragma warning restore CS8602
    }

    [Fact]
    public async Task ThenUpdateUserAccounts()
    {
#pragma warning disable CS8602
        //Assign 
        UserAccountDto userAccountDto = TestDataProvider.GetUserAccount();
        List<UserAccountDto> listUserAccounts = [userAccountDto];
        UserAccount userAccount = Mapper.Map<UserAccount>(userAccountDto);
        userAccount.OrganisationUserAccounts = Mapper.Map<List<UserAccountOrganisation>>(userAccountDto.OrganisationUserAccounts);
        TestDbContext.Organisations.Add(userAccount.OrganisationUserAccounts[0].Organisation);
        await TestDbContext.SaveChangesAsync();
        userAccount.OrganisationUserAccounts[0].Organisation = TestDbContext.Organisations.First(x => x.Id == userAccount.OrganisationUserAccounts[0].Organisation.Id);
        TestDbContext.UserAccounts.Add(userAccount);
        await TestDbContext.SaveChangesAsync();

        userAccountDto.EmailAddress = "UpdatedUser@email.com";
        userAccountDto.PhoneNumber = "0161 111 1112";

        UpdateUserAccountsCommand command = new UpdateUserAccountsCommand(listUserAccounts);
        UpdateUserAccountsCommandHandler handler = new UpdateUserAccountsCommandHandler(TestDbContext, Mapper);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().BeTrue();
        var actualUserAccount = await TestDbContext.UserAccounts
            .Include(x => x.OrganisationUserAccounts)
            .FirstAsync(x => x.OrganisationUserAccounts != null && x.OrganisationUserAccounts.Any(uao => uao.OrganisationId == userAccountDto.OrganisationUserAccounts[0].Organisation.Id));

        actualUserAccount.EmailAddress.Should().Be(userAccount.EmailAddress);
        actualUserAccount.PhoneNumber.Should().Be(userAccount.PhoneNumber);
        actualUserAccount.OrganisationUserAccounts[0].OrganisationId.Should().Be(userAccountDto.OrganisationUserAccounts[0].Organisation.Id);


#pragma warning restore CS8602
    }

    
    [Fact]
    public async Task ThenGetUserById()
    {
#pragma warning disable CS8602
        //Assign 
        GetUserByIdCommand command = new (5);
        GetUserByIdCommandHandler handler = new(TestDbContext, Mapper);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Should().NotBeNull();
        var actualUserAccount = await TestDbContext.UserAccounts
            .Include(x => x.OrganisationUserAccounts)
            .FirstAsync(x => x.Id == 5);

        actualUserAccount.EmailAddress.Should().Be(ReferralSeedData.SeedReferral().ElementAt(0).UserAccount.EmailAddress);
        actualUserAccount.PhoneNumber.Should().Be(ReferralSeedData.SeedReferral().ElementAt(0).UserAccount.PhoneNumber);
        


#pragma warning restore CS8602
    }
    

    [Fact]
    public async Task ThenGetUserAccountByOrganisationId()
    {
#pragma warning disable CS8602
        //Assign 
        UserAccountDto userAccountDto = TestDataProvider.GetUserAccount();
        UserAccount userAccount = Mapper.Map<UserAccount>(userAccountDto);
        userAccount.OrganisationUserAccounts = Mapper.Map<List<UserAccountOrganisation>>(userAccountDto.OrganisationUserAccounts);
        TestDbContext.Organisations.Add(userAccount.OrganisationUserAccounts[0].Organisation);
        await TestDbContext.SaveChangesAsync();
        userAccount.OrganisationUserAccounts[0].Organisation = TestDbContext.Organisations.First(x => x.Id == userAccount.OrganisationUserAccounts[0].Organisation.Id);
        TestDbContext.UserAccounts.Add(userAccount);
        await TestDbContext.SaveChangesAsync();

        GetUsersByOrganisationIdCommand command = new GetUsersByOrganisationIdCommand(userAccount.OrganisationUserAccounts[0].Organisation.Id, 1, 10);
        GetUsersByOrganisationIdCommandHandler handler = new GetUsersByOrganisationIdCommandHandler(TestDbContext, Mapper);

        //Act
        var result = await handler.Handle(command, new CancellationToken());

        //Assert
        result.Items.Count.Should().BeGreaterThan(0);
        var actualUserAccount = await TestDbContext.UserAccounts
            .Include(x => x.OrganisationUserAccounts)
            .FirstAsync(x => x.OrganisationUserAccounts != null && x.OrganisationUserAccounts.Any(uao => uao.OrganisationId == userAccountDto.OrganisationUserAccounts[0].Organisation.Id));

        actualUserAccount.EmailAddress.Should().Be(userAccount.EmailAddress);
        actualUserAccount.PhoneNumber.Should().Be(userAccount.PhoneNumber);
        actualUserAccount.OrganisationUserAccounts[0].OrganisationId.Should().Be(userAccountDto.OrganisationUserAccounts[0].Organisation.Id);


#pragma warning restore CS8602
    }

    
}
