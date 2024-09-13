using System.Net;
using FamilyHubs.Report.Api.AcceptanceTests.Tests.Steps;
using TestStack.BDDfy;
using Xunit;

namespace FamilyHubs.Report.Api.AcceptanceTests.Tests.Stories;

public class ServiceSearchesForAnLaOrgTests
{
    private readonly SharedSteps _sharedSteps = new();
    private readonly ServiceSearchesForAnLaOrgSteps _steps = new();

    #region Service Searches in 7 days For An LA Org Tests

    //Service Searches in the Past 7 days GET Request As an LA Manager, LA Dual Role and VCS Manager (Find and Connect). 
    //Note: Find serviceTypeId =2 and For Connect serviceTypeId =1
    [Theory]
    [InlineData("LaManager", 6, "1", "2024-06-11", HttpStatusCode.OK)]
    [InlineData("LaDualRole", 6, "1", "2024-06-11", HttpStatusCode.OK)]
    [InlineData("VcsManager", 6, "1", "2024-06-11", HttpStatusCode.OK)]
    [InlineData("VcsDualRole", 6, "1", "2024-06-11", HttpStatusCode.OK)]
    [InlineData("LaManager", 6, "2", "2024-06-11", HttpStatusCode.OK)]
    [InlineData("LaDualRole", 6, "2", "2024-06-11", HttpStatusCode.OK)]
    [InlineData("VcsManager", 6, "2", "2024-06-11", HttpStatusCode.OK)]
    [InlineData("VcsDualRole", 6, "2", "2024-06-11", HttpStatusCode.OK)]  
    public void Service_Searches_In_The_Past_Seven_Days_For_An_La_Org_Returns_200_For_Valid_Role_Type(string role,
        int laOrgId, string serviceTypeId, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForSearchesInThePastSevenDaysForAnLaOrg(laOrgId, serviceTypeId, date,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches in the Past 7 days GET Request As a LA Manager, LA Dual Role and VCS Manager with an invalid bearer token Returns a 401 Unauthorized (Find and Connect)
    [Theory]
    [InlineData("LaManager", 6, "1", "2024-06-11", HttpStatusCode.Unauthorized)]
    [InlineData("LaDualRole", 6, "1", "2024-06-11", HttpStatusCode.Unauthorized)]
    [InlineData("VcsManager", 6, "1", "2024-06-11", HttpStatusCode.Unauthorized)]
    [InlineData("VcsDualRole", 6, "1", "2024-06-11", HttpStatusCode.Unauthorized)]
    [InlineData("LaManager", 6, "2", "2024-06-11", HttpStatusCode.Unauthorized)]
    [InlineData("LaDualRole", 6, "2", "2024-06-11", HttpStatusCode.Unauthorized)]
    [InlineData("VcsManager", 6, "2", "2024-06-11", HttpStatusCode.Unauthorized)]
    [InlineData("VcsDualRole", 6, "2", "2024-06-11", HttpStatusCode.Unauthorized)]
    public void Service_Searches_In_The_Past_Seven_Days_For_An_La_Org_Returns_401_With_An_Invalid_Bearer_Token(
        string role, int laOrgId, string serviceTypeId, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.HaveAnInvalidBearerToken())
            .When(s => _steps.SendAValidRequestForSearchesInThePastSevenDaysForAnLaOrg(laOrgId, serviceTypeId, date,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches in the Past 7 days GET Request With a role other than LA Manager,LA Dual Role, VCS Dual Role or VCS Manager returns 403 Forbidden (Find and Connect)
    [Theory]
    [InlineData("DfeAdmin", 6, "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("LaProfessional", 6, "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("VcsProfessional", 6, "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("ServiceAccount", 6, "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("DfeAdmin", 6, "2", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("LaProfessional", 6, "2", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("VcsProfessional", 6, "2", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("ServiceAccount", 6, "2", "2024-06-11", HttpStatusCode.Forbidden)]
    public void
        Service_Searches_In_The_Past_Seven_Days_For_An_La_Org_Error_Returns_403_When_Unsupported_Role_Is_Requested(
            string role, int laOrgId, string serviceTypeId, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForSearchesInThePastSevenDaysForAnLaOrg(laOrgId, serviceTypeId, date,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches in the Past 7 days GET Request returns 422 when the serviceTypeId parameter is missing
    [Theory]
    [InlineData("LaManager", 6, "2024-06-11", HttpStatusCode.UnprocessableEntity)]
    public void
        Service_Searches_In_The_Past_Seven_Days_For_An_La_Org_Returns_422_Error_For_Missing_SystemTypeId_Parameter(
            string role, int laOrgId, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s =>
                _steps.SendAValidRequestForSearchesInThePastSevenDaysForAnLaOrgWithoutServiceTypeIdParameter(laOrgId,
                    date, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches in the Past 7 days GET Request returns 422 when the date parameter is missing
    [Theory]
    [InlineData("LaManager", 6, "1", HttpStatusCode.UnprocessableEntity)]
    public void Service_Searches_In_The_Past_Seven_Days_For_An_La_Org_Returns_422_Error_For_Missing_date_Parameter(
        string role, int laOrgId, string serviceTypeId, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForSearchesInThePastSevenDaysForAnLaOrgWithoutDateParameter(laOrgId,
                serviceTypeId, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches in the Past 7 days GET Request Returns a 422 for unsupported serviceTypeID parameter
    [Theory]
    [InlineData("LaManager", 6, "4", "2024-06-11", HttpStatusCode.UnprocessableEntity)]
    [InlineData("LaDualRole", 6, "3", "2024-06-11", HttpStatusCode.UnprocessableEntity)]
    [InlineData("VcsManager", 6, "3", "2024-06-11", HttpStatusCode.UnprocessableEntity)]
    [InlineData("VcsDualRole", 6, "3", "2024-06-11", HttpStatusCode.UnprocessableEntity)]
    public void Service_Searches_In_The_Past_Seven_Days_For_An_La_Org_Returns_422_For_Unsupported_ServiceTypeId(
        string role, int laOrgId, string serviceTypeId, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForSearchesInThePastSevenDaysForAnLaOrg(laOrgId, serviceTypeId, date,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    #endregion Service Searches in 7 days For An LA Org Tests

    #region Service Searches 4 Week Breakdown For An LA Org Tests

    //Get Request for Service Searches 4 Week Breakdown For An LA Org
    [Theory]
    [InlineData("LaManager", 6, "1", "2024-06-11", HttpStatusCode.OK)]
    [InlineData("LaDualRole", 6, "1", "2024-06-11", HttpStatusCode.OK)]
    [InlineData("VcsManager", 6, "1", "2024-06-11", HttpStatusCode.OK)]
    [InlineData("VcsDualRole", 6, "1", "2024-06-11", HttpStatusCode.OK)]
    [InlineData("LaManager", 6, "2", "2024-06-11", HttpStatusCode.OK)]
    [InlineData("LaDualRole", 6, "2", "2024-06-11", HttpStatusCode.OK)]
    [InlineData("VcsManager", 6, "2", "2024-06-11", HttpStatusCode.OK)]
    [InlineData("VcsDualRole", 6, "2", "2024-06-11", HttpStatusCode.OK)]
    public void Service_Searches_For_Week_Breakdown_For_An_La_Org_Returns_200(string role, int laOrgId,
        string serviceTypeId, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForFourWeekBreakdownForAnLaOrg(laOrgId, serviceTypeId, date,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches 4 Week Breakdown For An LA Org GET Request As a LA Manager or LA Dual Role Returns a 401 When using an invalid bearer token (Find and Connect)
    [Theory]
    [InlineData("LaManager", 6, "1", "2024-06-11", HttpStatusCode.Unauthorized)]
    [InlineData("LaDualRole", 6, "1", "2024-06-11", HttpStatusCode.Unauthorized)]
    [InlineData("VcsManager", 6, "1", "2024-06-11", HttpStatusCode.Unauthorized)]
    [InlineData("VcsDualRole", 6, "1", "2024-06-11", HttpStatusCode.Unauthorized)]
    [InlineData("LaManager", 6, "2", "2024-06-11", HttpStatusCode.Unauthorized)]
    [InlineData("LaDualRole", 6, "2", "2024-06-11", HttpStatusCode.Unauthorized)]
    [InlineData("VcsManager", 6, "2", "2024-06-11", HttpStatusCode.Unauthorized)]
    [InlineData("VcsDualRole", 6, "2", "2024-06-11", HttpStatusCode.Unauthorized)]
    public void Service_Searches_For_Week_Breakdown_For_An_La_Org_Returns_401_With_Invalid_Bearer_Token(string role,
        int laOrgId, string serviceTypeId, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.HaveAnInvalidBearerToken())
            .When(s => _steps.SendAValidRequestForSearchesInThePastSevenDaysForAnLaOrg(laOrgId, serviceTypeId, date,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches 4 Week Breakdown For An LA Org GET Request As a role other than LA Manager, VCS Manager, VCS Dual Role or LA Dual Role returns a 403 (Find and Connect)
    [Theory]
    [InlineData("DfeAdmin", 6, "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("LaProfessional", 6, "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("VcsProfessional", 6, "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("ServiceAccount", 6, "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("DfeAdmin", 6, "2", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("LaProfessional", 6, "2", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("VcsProfessional", 6, "2", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("ServiceAccount", 6, "2", "2024-06-11", HttpStatusCode.Forbidden)]
    public void Service_Searches_For_Week_Breakdown_For_An_La_Org_Error_Messages_For_Missing_Parameters(string role,
        int laOrgId, string serviceTypeId, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForSearchesInThePastSevenDaysForAnLaOrg(laOrgId, serviceTypeId, date,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches in the Past 7 days GET Request returns 422 when the systemTypeID parameter is missing
    [Theory]
    [InlineData("LaManager", 6, "2024-06-11", HttpStatusCode.UnprocessableEntity)]
    public void Service_Searches_For_Week_Breakdown_For_An_La_Org_Returns_422_With_Invalid_Bearer_Token(string role,
        int laOrgId, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForFourWeekBreakdownForAnLaOrgWithServiceTypeIdParameterMissing(laOrgId,
                date, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches in the Past 7 days GET Request returns 422 when the date parameter is missing
    [Theory]
    [InlineData("LaManager", 6, "1", HttpStatusCode.UnprocessableEntity)]
    public void Service_Searches_For_Week_Breakdown_For_An_La_Org_Returns_422_Error_When_Date_Parameter_Is_Missing(
        string role, int laOrgId, string serviceTypeId, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForFourWeekBreakdownForAnLaOrgWithDateParameterMissing(laOrgId,
                serviceTypeId, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches in the Past 7 days GET Request returns 422 when an unsupported serviceTypeID parameter is entered
    [Theory]
    [InlineData("LaManager", 6, "4", "2024-06-11", HttpStatusCode.UnprocessableEntity)]
    [InlineData("LaDualRole", 6, "3", "2024-06-11", HttpStatusCode.UnprocessableEntity)]
    [InlineData("VcsManager", 6, "3", "2024-06-11", HttpStatusCode.UnprocessableEntity)]
    [InlineData("VcsDualRole", 6, "3", "2024-06-11", HttpStatusCode.UnprocessableEntity)]
    public void Service_Searches_For_Week_Breakdown_For_An_La_Org_422_Error_When_ServiceTypeID_Parameter_Is_Unsupported(
        string role, int laOrgId, string serviceTypeId, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForFourWeekBreakdownForAnLaOrg(laOrgId, serviceTypeId, date,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    #endregion Service Searches 4 Week Breakdown For An LA Org Tests

    #region Service Searches Total For An LA Org Tests

    //Get Request for Service Searches Total For An LA Org
    [Theory]
    [InlineData("LaManager", 6, "1", HttpStatusCode.OK)]
    [InlineData("LaDualRole", 6, "1", HttpStatusCode.OK)]
    [InlineData("VcsManager", 6, "1", HttpStatusCode.OK)]
    [InlineData("VcsDualRole", 6, "1", HttpStatusCode.OK)]
    [InlineData("LaManager", 6, "2", HttpStatusCode.OK)]
    [InlineData("LaDualRole", 6, "2", HttpStatusCode.OK)]
    [InlineData("VcsManager", 6, "2", HttpStatusCode.OK)]
    [InlineData("VcsDualRole", 6, "2", HttpStatusCode.OK)]
    public void Service_SearchesTotal_For_An_La_Org_Returns_200(string role, int laOrgId, string serviceTypeId,
        HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForServiceSearchTotalsForAnLaOrg(laOrgId, serviceTypeId,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches Total For An LA Org GET Request As a LA Manager or LA Dual Role Returns a 500 with an invalid bearer token (Find and Connect)
    [Theory]
    [InlineData("LaManager", 6, "1", HttpStatusCode.Unauthorized)]
    [InlineData("LaDualRole", 6, "1", HttpStatusCode.Unauthorized)]
    [InlineData("VcsManager", 6, "1", HttpStatusCode.Unauthorized)]
    [InlineData("VcsDualRole", 6, "1", HttpStatusCode.Unauthorized)]
    [InlineData("LaManager", 6, "2", HttpStatusCode.Unauthorized)]
    [InlineData("LaDualRole", 6, "2", HttpStatusCode.Unauthorized)]
    [InlineData("VcsManager", 6, "2", HttpStatusCode.Unauthorized)]
    [InlineData("VcsDualRole", 6, "2", HttpStatusCode.Unauthorized)]
    public void Service_SearchesTotal_For_An_La_Org_Returns_400_With_Invalid_Bearer_Token(string role, int laOrgId,
        string serviceTypeId, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.HaveAnInvalidBearerToken())
            .When(s => _steps.SendAValidRequestForServiceSearchTotalsForAnLaOrg(laOrgId, serviceTypeId,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches Total For An LA Org GET Request As a role other than LA Manager,LA Dual Role, VCS Dual Role or VCS Manager returns a 403
    [Theory]
    [InlineData("DfeAdmin", 6, "1", HttpStatusCode.Forbidden)]
    [InlineData("LaProfessional", 6, "1", HttpStatusCode.Forbidden)]
    [InlineData("VcsProfessional", 6, "1", HttpStatusCode.Forbidden)]
    [InlineData("ServiceAccount", 6, "1", HttpStatusCode.Forbidden)]
    [InlineData("DfeAdmin", 6, "2", HttpStatusCode.Forbidden)]
    [InlineData("LaProfessional", 6, "2", HttpStatusCode.Forbidden)]
    [InlineData("VcsProfessional", 6, "2", HttpStatusCode.Forbidden)]
    [InlineData("ServiceAccount", 6, "2", HttpStatusCode.Forbidden)]
    public void Service_SearchesTotal_For_An_La_Org_Returns_403_For_Unsupported_Roles(string role, int laOrgId,
        string serviceTypeId, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForServiceSearchTotalsForAnLaOrg(laOrgId, serviceTypeId,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches Total GET Request returns 422 when the systemTypeID parameter is missing
    [Theory]
    [InlineData("LaManager", 6, HttpStatusCode.UnprocessableEntity)]
    public void Service_SearchesTotal_For_An_La_Org_Returns_422_With_Invalid_Bearer_Token(string role, int laOrgId,
        HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForServiceSearchTotalsForAnLaOrgWithSystemTypeIdParameterMissing(laOrgId,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    [Theory]
    [InlineData("LaManager", 6, "3", HttpStatusCode.UnprocessableEntity)]
    [InlineData("LaDualRole", 6, "3", HttpStatusCode.UnprocessableEntity)]
    [InlineData("VcsManager", 6, "3", HttpStatusCode.UnprocessableEntity)]
    [InlineData("VcsDualRole", 6, "3", HttpStatusCode.UnprocessableEntity)]
    public void Service_SearchesTotal_For_An_La_Org_Returns_400_Invalid_ServiceTypeId(string role, int laOrgId,
        string serviceTypeId, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForServiceSearchTotalsForAnLaOrg(laOrgId, serviceTypeId,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    #endregion Service Searches Total For An LA Org Tests
}