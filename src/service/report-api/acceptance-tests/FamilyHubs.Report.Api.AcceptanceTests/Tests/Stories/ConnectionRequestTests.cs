using System.Net;
using FamilyHubs.Report.Api.AcceptanceTests.Tests.Steps;
using TestStack.BDDfy;
using Xunit;

namespace FamilyHubs.Report.Api.AcceptanceTests.Tests.Stories;

public class ConnectionRequestTests
{
    private readonly SharedSteps _sharedSteps;
    private readonly ConnectionRequestsOverallSteps _steps;
    
    public ConnectionRequestTests()
    {
        _steps = new ConnectionRequestsOverallSteps();
        _sharedSteps = new SharedSteps();
    }

    #region Total Connection Requests

    //Total Connections Made 
    [Theory]
    [InlineData("DfeAdmin", HttpStatusCode.OK)]
    public void Connection_Requests_Total_Endpoint_Returns_200_For_Successful_Request_And_Verifying_Response_Entry_Names(
            string role, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForTheTotalConnectionRequests(_sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .Then(s => _steps.VerifySevenDaysConnectionRequestResponseBody(_steps.LastResponse))
            .BDDfy();
    }

    //Negative Scenarios
    //Total Connections Made with invalid role type 
    [Theory]
    [InlineData("LaManager", HttpStatusCode.Forbidden)]
    [InlineData("VcsManager", HttpStatusCode.Forbidden)]
    [InlineData("LaProfessional", HttpStatusCode.Forbidden)]
    [InlineData("VcsProfessional", HttpStatusCode.Forbidden)]
    [InlineData("VcsDualRole", HttpStatusCode.Forbidden)]
    [InlineData("LaDualRole", HttpStatusCode.Forbidden)]
    [InlineData("ServiceAccount", HttpStatusCode.Forbidden)]
    public void Connection_Requests_Total_Endpoint_Returns_403_Forbidden_For_Invalid_Role_Types(string role,
        HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForTheTotalConnectionRequests(_sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Total Connections Made with an invalid bearer token
    [Theory]
    [InlineData(HttpStatusCode.Unauthorized)]
    public void Connection_Requests_Total_Endpoint_Returns_401_Unauthorized_Error_For_An_Invalid_Bearer_Token(
        HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.HaveAnInvalidBearerToken())
            .When(s => _steps.SendAValidRequestForTheTotalConnectionRequests(_sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    #endregion Total Connection Requests

    #region Connection Requests in the Past 7 Days Tests

    //Connection Requests in the Past 7 days 
    [Theory]
    [InlineData("DfeAdmin", "2024-06-11", HttpStatusCode.OK)]
    public void Connection_Requests_In_The_Past_Seven_Days_Endpoint_Returns_200_OK_And_Verifying_Response_Entry_Names(
        string role, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForConnectionRequestsInThePast7days(date, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .And(s => _steps.VerifySevenDaysConnectionRequestResponseBody(_steps.LastResponse))
            .BDDfy();
    }


    //Negative Scenarios
    //Connection Requests in the Past 7 days returns an error with invalid role type 
    [Theory]
    [InlineData("2024-06-11", HttpStatusCode.Unauthorized)]
    public void Connection_Requests_In_The_Past_Seven_Days_Endpoint_Returns_401_Unauthorized_For_Invalid_Bearer_Token(
        string date,
        HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.HaveAnInvalidBearerToken())
            .When(s => _steps.SendAValidRequestForConnectionRequestsInThePast7days(date, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Connection Requests in the Past 7 days returns an error with unsupported role type
    [Theory]
    [InlineData("LaManager", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("VcsManager", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("LaProfessional", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("VcsProfessional", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("VcsDualRole", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("LaDualRole", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("ServiceAccount", "2024-06-11", HttpStatusCode.Forbidden)]
    public void Connection_Requests_In_The_Past_Seven_Days_Endpoint_Returns_403_Forbidden_For_Unsupported_Role_Type(
        string role, string date,
        HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForConnectionRequestsInThePast7days(date, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Connection Requests in the Past 7 days returns an error with missing date parameter 
    [Theory]
    [InlineData("DfeAdmin", HttpStatusCode.UnprocessableEntity)]
    public void Connection_Requests_In_The_Past_Seven_Days_Endpoint_Returns_422_Unauthorized_For_Missing_Date_Parameter(
        string role,
        HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s =>
                _steps.SendAValidRequestForConnectionRequestsInThePast7daysWithoutDateParameter(
                    _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    #endregion Connection Requests in the Past 7 Days Tests

    #region Connection Requests 4 week Breakdown Tests

    //Connection Requests 4 week Breakdown Tests Successful Request 
    [Theory]
    [InlineData("DfeAdmin", "2024-06-11", HttpStatusCode.OK)]
    public void
        Connection_Requests_Four_Week_Breakdown_Endpoint_Returns_200_And_Verifying_Totals_Section_Of_Response_EntryNames(
            string role, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForConnectionRequestsFourWeekBreakdown(date, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .And(s => _steps.VerifyTotalsSectionOfTheFourWeekBreakDownResponseBody(_steps.LastResponse))
            .BDDfy();
    }

    [Theory]
    [InlineData("DfeAdmin", "2024-06-11", HttpStatusCode.OK)]
    public void
        Connection_Requests_Four_Week_Breakdown_Endpoint_Returns_200_And_Verifying_Weekly_Reports_Section_Of_Response_EntryNames(
            string role, string date, HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForConnectionRequestsFourWeekBreakdown(date, _sharedSteps.BearerToken))
            .Then(s => _steps.VerifyWeeklyReportsSectionOfTheFourWeekBreakDownResponseBody(_steps.LastResponse))
            .And(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Negative Scenarios
    //Connection Requests 4 week Breakdown returns an error with invalid bearer token 
    [Theory]
    [InlineData("2024-06-11", HttpStatusCode.Unauthorized)]
    public void Connection_Requests_Four_Week_Breakdown_Endpoint_Returns_401_Unauthorized_For_Invalid_Bearer_Token(
        string date,
        HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.HaveAnInvalidBearerToken())
            .When(s => _steps.SendAValidRequestForConnectionRequestsFourWeekBreakdown(date, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Connection Requests 4 week Breakdown returns an error for invalid role type 
    [Theory]
    [InlineData("LaManager", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("VcsManager", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("LaProfessional", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("VcsProfessional", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("VcsDualRole", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("LaDualRole", "2024-06-11", HttpStatusCode.Forbidden)]
    [InlineData("ServiceAccount", "2024-06-11", HttpStatusCode.Forbidden)]
    public void Connection_Requests_Four_Week_Breakdown_Endpoint_Returns_403_Forbidden_For_Unsupported_Role_Types(
        string role, string date,
        HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s => _steps.SendAValidRequestForConnectionRequestsFourWeekBreakdown(date, _sharedSteps.BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    //Connection Requests 4 week Breakdown returns an error for missing date parameter
    [Theory]
    [InlineData("DfeAdmin", HttpStatusCode.UnprocessableEntity)]
    public void
        Connection_Requests_Four_Week_Breakdown_Endpoint_Returns_422_Unprocessable_Entity_For_Missing_Date_Parameter(
            string role,
            HttpStatusCode statusCode)
    {
        this.Given(s => _sharedSteps.GenerateBearerToken(role))
            .When(s =>
                _steps.SendAValidRequestForConnectionRequestsFourWeekBreakdownWithAMissingDateParameter(_sharedSteps
                    .BearerToken))
            .Then(s => _sharedSteps.VerifyStatusCode(_steps.LastResponse, statusCode))
            .BDDfy();
    }

    #endregion Connection Requests 4 week Breakdown Tests
}