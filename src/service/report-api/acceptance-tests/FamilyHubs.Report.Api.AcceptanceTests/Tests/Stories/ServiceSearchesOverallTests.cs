using System.Net;
using FamilyHubs.Report.Api.AcceptanceTests.Tests.Steps;
using TestStack.BDDfy;
using Xunit;

namespace FamilyHubs.Report.Api.AcceptanceTests.Tests.Stories;

public class ServiceSearchesOverallTests
{
    private readonly SharedSteps _sharedSteps = new();
    private readonly ServiceSearchesOverallSteps _steps = new();

    #region Overall Service Searches in the Past 7 Days Tests

    //Service Searches in the Past 7 days GET Request for Find and Connect
    //Note: Find serviceTypeId =2 and For Connect serviceTypeId =1
    [Theory]
    [InlineData("DfeAdmin", "1", "2024-06-11", HttpStatusCode.OK)]
    [InlineData("DfeAdmin", "2", "2024-06-11", HttpStatusCode.OK)]
    public void Searches_In_Past_Seven_Days_Returns_Searches_Returns_200(string role, string serviceTypeId, string date,
        HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForPast7days(serviceTypeId, date, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Negative scenarios
    //Service Searches in the Past 7 days GET Request returns a 401 with an Invalid Bearer Token 
    [Theory]
    [InlineData("1", "2024-06-11", HttpStatusCode.Unauthorized)]
    [InlineData("2", "2024-06-11", HttpStatusCode.Unauthorized)]
    public void Searches_In_Past_Seven_Days_Returns_Searches_Returns_401_Error_With_Invalid_Bearer_Token(
        string serviceTypeId, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.HaveAnInvalidBearerToken())
            .When(s => _steps.SendAValidRequestForPast7days(serviceTypeId, date, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches in the Past 7 days GET Request with No serviceTypeId Parameter Returns a 422 
    [Theory]
    [InlineData("DfeAdmin", "2024-06-11", HttpStatusCode.UnprocessableEntity)]
    public void Searches_In_Past_Seven_Days_Returns_Searches_Returns_422_Error_For_Missing_ServiceTypeId_Parameters(
        string role, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForPast7daysWithoutServiceTypeId(date, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches in the Past 7 days GET Request with No date Parameter Returns a 422 
    [Theory]
    [InlineData("DfeAdmin", "2", HttpStatusCode.UnprocessableEntity)]
    public void Searches_In_Past_Seven_Days_Returns_Searches_Returns_422_Error_For_Missing_Date_Parameters(string role,
        string serviceTypeId, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(
                s => _steps.SendAValidRequestForPast7daysWithoutDateParameter(serviceTypeId, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches in the Past 7 days GET Request returns a 403 for un-supported role types 
    [Theory]
    [InlineData("LaManager", "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("VcsManager", "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("LaProfessional", "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("VcsProfessional", "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("VcsDualRole", "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("LaDualRole", "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("ServiceAccount", "1", "2024-06-11", HttpStatusCode.Forbidden)]
    public void Searches_In_Past_Seven_Days_Returns_Searches_Returns_403_Error_For_Unsupported_Role_Type(string role,
        string serviceTypeId, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForPast7days(serviceTypeId, date, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches in the Past 7 days GET Request returns a 422 for an invalid ServiceType ID entry
    [Theory]
    [InlineData("DfeAdmin", "3", "2024-06-11", HttpStatusCode.UnprocessableEntity)]
    public void Searches_In_Past_Seven_Days_Returns_Searches_Returns_422_When_Invalid_ServiceTypeId(string role,
        string serviceTypeId, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForPast7days(serviceTypeId, date, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    #endregion Overall Service Searches in the Past 7 Days Tests

    #region Service Searches 4 Week Breakdown Tests

    //Get Request for Service Searches 4 Week Breakdown Endpoint For Find and Connect
    [Theory]
    [InlineData("DfeAdmin", "1", "2024-06-11", HttpStatusCode.OK)]
    [InlineData("DfeAdmin", "2", "2024-06-11", HttpStatusCode.OK)]
    public void Searches_In_Past_Four_Weeks_Returns_Searches_Returns_200(string role, string serviceTypeId, string date,
        HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForServiceSearches4WeekBreakdown(serviceTypeId, date,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //4 Week Breakdown Negative Tests 
    //Service Searches 4 week breakdown GET request returns a 401 For An Invalid Bearer Token (Find and Connect)
    [Theory]
    [InlineData("1", "2024-06-11", HttpStatusCode.Unauthorized)]
    [InlineData("2", "2024-06-11", HttpStatusCode.Unauthorized)]
    public void Searches_In_Past_Four_Weeks_Returns_Searches_Returns_401_For_Invalid_Token(string serviceTypeId,
        string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.HaveAnInvalidBearerToken())
            .When(s => _steps.SendAValidRequestForServiceSearches4WeekBreakdown(serviceTypeId, date,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    // Service Searches 4 week breakdown GET Requests returns a 422 with missing ServiceTypeID Parameter 
    [Theory]
    [InlineData("DfeAdmin", "2024-06-11", HttpStatusCode.UnprocessableEntity)]
    public void Searches_In_Past_Four_Weeks_Returns_Searches_Returns_422_For_Missing_Parameters(string role,
        string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForServiceSearches4WeekBreakdownWithoutServiceTypeId(date,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    // Service Searches 4 week breakdown GET Requests returns a 422 with missing date Parameter 
    [Theory]
    [InlineData("DfeAdmin", "1", HttpStatusCode.UnprocessableEntity)]
    [InlineData("DfeAdmin", "2", HttpStatusCode.UnprocessableEntity)]
    public void Searches_In_Past_Four_Weeks_Returns_Searches_Returns_500_For_Missing_Parameters(string role,
        string serviceTypeId, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForServiceSearches4WeekBreakdownWithoutDateParameters(serviceTypeId,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    // Service Searches 4 week breakdown GET Requests returns a 403 for un-supported role types
    [Theory]
    [InlineData("LaManager", "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("VcsManager", "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("LaProfessional", "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("VcsProfessional", "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("VcsDualRole", "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("LaDualRole", "1", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("ServiceAccount", "1", "2024-06-11", HttpStatusCode.Forbidden)]
    public void Searches_In_Past_Four_Weeks_Returns_Searches_Returns_403_For_Unsupported_Role_Type(string role,
        string serviceTypeId, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForServiceSearches4WeekBreakdown(serviceTypeId, date,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    // Service Searches 4 week breakdown GET Requests returns a 422 with an invalid ServiceTypeID Parameter 
    [Theory]
    [InlineData("DfeAdmin", "3", "2024-06-11", HttpStatusCode.UnprocessableEntity)]
    public void Searches_In_Past_Four_Weeks_Returns_Searches_Returns_200_asd(string role, string serviceTypeId,
        string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForServiceSearches4WeekBreakdown(serviceTypeId, date,
                _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    #endregion Service Searches 4 Week Breakdown Tests

    #region Service Searches Total Tests

    //Get Request for Service Searches Total Endpoint For Find and Connect
    [Theory]
    [InlineData("DfeAdmin", "1", HttpStatusCode.OK)]
    [InlineData("DfeAdmin", "2", HttpStatusCode.OK)]
    public void Service_Searches_Total_Returns_200(string role, string serviceTypeId, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForServiceSearchesTotal(serviceTypeId, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Negative Scenarios
    // Service Searches Total GET Request Returns a 401 with An Invalid Bearer Token 
    [Theory]
    [InlineData("1", HttpStatusCode.Unauthorized)]
    [InlineData("2", HttpStatusCode.Unauthorized)]
    public void Service_Searches_Total_Returns_401_Error_With_An_Invalid_Bearer_Token(string serviceTypeId,
        HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.HaveAnInvalidBearerToken())
            .When(s => _steps.SendAValidRequestForServiceSearchesTotal(serviceTypeId, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches Total GET Request Returns a 422 with Missing ServiceTypeId Parameter 
    [Theory]
    [InlineData("DfeAdmin", HttpStatusCode.UnprocessableEntity)]
    public void Service_Searches_Total_Returns_500_Error_For_Missing_Parameters(string role, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s =>
                _steps.SendAValidRequestForServiceSearchesTotalWithoutServiceTypeIdParameters(_sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches Total GET Request Returns a 422 when an Invalid ServiceTypeId Parameter 
    [Theory]
    [InlineData("DfeAdmin", "3", HttpStatusCode.UnprocessableEntity)]
    public void Service_Searches_Total_Returns_Invalid_Total(string role, string serviceTypeId,
        HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForServiceSearchesTotal(serviceTypeId, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Service Searches Total GET Request returns a 403 with un-supported role types
    [Theory]
    [InlineData("LaManager", "1", HttpStatusCode.Forbidden)]
    [InlineData("VcsManager", "1", HttpStatusCode.Forbidden)]
    [InlineData("LaProfessional", "1", HttpStatusCode.Forbidden)]
    [InlineData("VcsProfessional", "1", HttpStatusCode.Forbidden)]
    [InlineData("VcsDualRole", "1", HttpStatusCode.Forbidden)]
    [InlineData("LaDualRole", "1", HttpStatusCode.Forbidden)]
    [InlineData("ServiceAccount", "1", HttpStatusCode.Forbidden)]
    public void Service_Searches_Total_Returns_403_For_Invalid_User_Roles(string role, string serviceTypeId,
        HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForServiceSearchesTotal(serviceTypeId, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    #endregion Service Searches Total Tests
}