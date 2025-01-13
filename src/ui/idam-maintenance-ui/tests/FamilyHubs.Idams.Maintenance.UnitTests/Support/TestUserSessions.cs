using FamilyHubs.Idams.Maintenance.Data.Entities;

namespace FamilyHubs.Idams.Maintenance.UnitTests.Support;

public static class TestUserSessions
{
    public static UserSession GetUserSession1() => new()
    {
        Id = 1,
        Sid = "Sid1",
        Email = "jd@temp.org",
        LastActive = DateTime.Now
    };
    
    public static List<UserSession> GetListOfTestUserSessions() => [GetUserSession1()];
}