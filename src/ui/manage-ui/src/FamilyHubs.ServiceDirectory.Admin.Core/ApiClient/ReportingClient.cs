﻿using FamilyHubs.ServiceDirectory.Admin.Core.Exceptions;
using FamilyHubs.SharedKernel.Exceptions;
using System.Net.Http.Json;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.SharedKernel.Reports.ConnectionRequests;
using FamilyHubs.SharedKernel.Reports.WeeklyBreakdown;

namespace FamilyHubs.ServiceDirectory.Admin.Core.ApiClient;

public interface IReportingClient
{
    Task<long> GetServicesSearchesPast7Days(ServiceType type, long? laOrganisationId = null, CancellationToken cancellationToken = default);
    Task<WeeklyReportBreakdown> GetServicesSearches4WeekBreakdown(ServiceType type, long? laOrganisationId = null, CancellationToken cancellationToken = default);
    Task<long> GetServicesSearchesTotal(ServiceType type, long? laOrganisationId = null, CancellationToken cancellationToken = default);

    Task<ConnectionRequests> GetConnectionRequestsPast7Days(ServiceType type, long? laOrganisationId = null, CancellationToken cancellationToken = default);
    Task<ConnectionRequestsBreakdown> GetConnectionRequests4WeekBreakdown(ServiceType type, long? laOrganisationId = null, CancellationToken cancellationToken = default);
    Task<ConnectionRequests> GetConnectionRequestsTotal(ServiceType type, long? laOrganisationId = null, CancellationToken cancellationToken = default);
}

public class ReportingClient : ApiService, IReportingClient
{
    public ReportingClient(HttpClient client)
        : base(client)
    {
    }

    private static async Task ValidateResponse(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            // TODO : handle failures without throwing errors
            var failure = await response.Content.ReadFromJsonAsync<ApiExceptionResponse<ValidationError>>();
            if (failure != null)
            {
                throw new ApiException(failure);
            }
            response.EnsureSuccessStatusCode();
        }
    }

    public async Task<long> GetServicesSearchesPast7Days(ServiceType type, long? laOrganisationId = null, CancellationToken cancellationToken = default)
    {
        var uri = laOrganisationId.HasValue
            ? $"report/service-searches-past-7-days/organisation/{laOrganisationId}"
            : "report/service-searches-past-7-days";

        return await DoRequest<long>(
            $"{Client.BaseAddress}{uri}?serviceTypeId={(int)type}&date={DateTime.Today:yyyy-MM-dd}",
            cancellationToken
        );
    }

    public async Task<WeeklyReportBreakdown> GetServicesSearches4WeekBreakdown(ServiceType type, long? laOrganisationId = null, CancellationToken cancellationToken = default)
    {
        var uri = laOrganisationId.HasValue
            ? $"report/service-searches-4-week-breakdown/organisation/{laOrganisationId}"
            : "report/service-searches-4-week-breakdown";

        return await DoRequest<WeeklyReportBreakdown>(
            $"{Client.BaseAddress}{uri}?serviceTypeId={(int)type}&date={DateTime.Today:yyyy-MM-dd}",
            cancellationToken
        );
    }

    public async Task<long> GetServicesSearchesTotal(ServiceType type, long? laOrganisationId = null, CancellationToken cancellationToken = default)
    {
        var uri = laOrganisationId.HasValue
            ? $"report/service-searches-total/organisation/{laOrganisationId}"
            : "report/service-searches-total";

        return await DoRequest<long>($"{Client.BaseAddress}{uri}?serviceTypeId={(int)type}", cancellationToken);
    }

    public async Task<ConnectionRequests> GetConnectionRequestsPast7Days(ServiceType type, long? laOrganisationId = null, CancellationToken cancellationToken = default)
    {
        var uri = laOrganisationId.HasValue
            ? $"report/connection-requests-past-7-days/organisation/{laOrganisationId}"
            : "report/connection-requests-past-7-days";

        return await DoRequest<ConnectionRequests>(
            $"{Client.BaseAddress}{uri}?serviceTypeId={(int)type}&date={DateTime.Today:yyyy-MM-dd}",
            cancellationToken
        );
    }

    public async Task<ConnectionRequestsBreakdown> GetConnectionRequests4WeekBreakdown(ServiceType type, long? laOrganisationId = null, CancellationToken cancellationToken = default)
    {
        var uri = laOrganisationId.HasValue
            ? $"report/connection-requests-4-week-breakdown/organisation/{laOrganisationId}"
            : "report/connection-requests-4-week-breakdown";

        return await DoRequest<ConnectionRequestsBreakdown>(
            $"{Client.BaseAddress}{uri}?serviceTypeId={(int)type}&date={DateTime.Today:yyyy-MM-dd}",
            cancellationToken
        );
    }

    public async Task<ConnectionRequests> GetConnectionRequestsTotal(ServiceType type, long? laOrganisationId = null, CancellationToken cancellationToken = default)
    {
        var uri = laOrganisationId.HasValue
            ? $"report/connection-requests-total/organisation/{laOrganisationId}"
            : "report/connection-requests-total";

        return await DoRequest<ConnectionRequests>($"{Client.BaseAddress}{uri}?serviceTypeId={(int)type}", cancellationToken);
    }

    private async Task<T> DoRequest<T>(string uri, CancellationToken cancellationToken)
    {
        using var response = await Client.GetAsync(uri, cancellationToken);

        await ValidateResponse(response);

        return (await response.Content.ReadFromJsonAsync<T>(cancellationToken: cancellationToken))!;
    }
}
