using FamilyHubs.Idams.Maintenance.Data.Entities;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Support;

public static class TestAccounts
{
    public static Account GetAccount1() => new()
    {
        Id = 1,
        Name = "Jane Doe",
        Email = "jd@temp.org", 
        Status = AccountStatus.Active,
        Claims = [
            new AccountClaim { AccountId = 1, Name = "OrganisationId", Value = "100" },
            new AccountClaim { AccountId = 1, Name = "role", Value = SharedKernel.Identity.RoleTypes.DfeAdmin }
        ]
    };
    
    public static Account GetAccount2() => new()
    {
        Id = 2,
        Name = "Fred Bloggs",
        Email = "fb@temp.org",
        Status = AccountStatus.Active
    };
    
    public static Account GetAccount3() => new()
    {
        Id = 3,
        Name = "John Deer",
        Email = "jd2@temp.org", 
        Status = AccountStatus.Active,
        Claims = [
            new AccountClaim { AccountId = 3, Name = "OrganisationId", Value = "101" },
            new AccountClaim { AccountId = 3, Name = "role", Value = SharedKernel.Identity.RoleTypes.LaProfessional }
        ]
    };
    
    public static Account GetAccount4() => new()
    {
        Id = 4,
        Name = "Jen Deer",
        Email = "jd3@temp.org", 
        Status = AccountStatus.Active,
        Claims = [
            new AccountClaim { AccountId = 4, Name = "role", Value = SharedKernel.Identity.RoleTypes.LaProfessional }
        ]
    };

    public static List<Account> GetListOfAccounts() => [GetAccount1(), GetAccount2(), GetAccount3(), GetAccount4()];
}