using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts;
using FamilyHubs.Report.Core.Queries.ConnectionRequestsSentFacts.Requests;
using FamilyHubs.Report.Core.Queries.ServiceSearchFacts;
using FamilyHubs.Report.Core.Queries.ServiceSearchFacts.Requests;
using FamilyHubs.ServiceDirectory.Shared.Enums;
using FamilyHubs.SharedKernel.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;

namespace FamilyHubs.Report.Api.Endpoints;

// TODO: Convert to Mediator Pattern

public class MinimalAdminReportEndPoints
{
    public void RegisterAdminReportEndPoints(WebApplication app)
    {
        app.MapGet("report/service-searches-past-7-days",
            [Authorize(Roles = RoleTypes.DfeAdmin)]
            async (
                DateTime? date,
                ServiceType? serviceTypeId,
                IGetServiceSearchFactQuery getServiceSearchFactQuery,
                IValidator<SearchCountRequest> validator
            ) =>
            {
                SearchCountRequest request = new(date, serviceTypeId, 7);
                await validator.ValidateAndThrowAsync(request);
                return await getServiceSearchFactQuery.GetSearchCountForAdmin(request);
            });

        app.MapGet("report/service-searches-4-week-breakdown",
            [Authorize(Roles = RoleTypes.DfeAdmin)]
            async (
                DateTime? date,
                ServiceType? serviceTypeId,
                IGetFourWeekBreakdownQuery getFourWeekBreakdownQuery,
                IValidator<SearchBreakdownRequest> validator
            ) =>
            {
                SearchBreakdownRequest request = new(date, serviceTypeId);
                await validator.ValidateAndThrowAsync(request);
                return await getFourWeekBreakdownQuery.GetFourWeekBreakdownForAdmin(request);
            });

        app.MapGet("report/service-searches-total",
            [Authorize(Roles = RoleTypes.DfeAdmin)]
            async (
                ServiceType? serviceTypeId,
                IGetServiceSearchFactQuery getServiceSearchFactQuery,
                IValidator<TotalSearchCountRequest> validator
            ) =>
            {
                TotalSearchCountRequest request = new(serviceTypeId);
                await validator.ValidateAndThrowAsync(request);
                return await getServiceSearchFactQuery.GetTotalSearchCountForAdmin(request);
            });

        app.MapGet("report/connection-requests-past-7-days",
            [Authorize(Roles = RoleTypes.DfeAdmin)]
            async (
                DateTime? date,
                IGetConnectionRequestsSentFactQuery getConnectionRequestsSentFactQuery,
                IValidator<ConnectionRequestsRequest> validator) =>
            {
                ConnectionRequestsRequest request = new(date, 7);
                await validator.ValidateAndThrowAsync(request);
                return await getConnectionRequestsSentFactQuery.GetConnectionRequestsForAdmin(request);
            });


        app.MapGet("report/connection-requests-4-week-breakdown",
            [Authorize(Roles = RoleTypes.DfeAdmin)]
            async (DateTime? date,
                IGetConnectionRequestsSentFactFourWeekBreakdownQuery
                    getConnectionRequestsSentFactFourWeekBreakdownQuery,
                IValidator<ConnectionRequestsBreakdownRequest> validator) =>
            {
                ConnectionRequestsBreakdownRequest request = new(date);
                await validator.ValidateAndThrowAsync(request);
                return await getConnectionRequestsSentFactFourWeekBreakdownQuery.GetFourWeekBreakdownForAdmin(request);
            });


        app.MapGet("report/connection-requests-total",
            [Authorize(Roles = RoleTypes.DfeAdmin)]
            async (IGetConnectionRequestsSentFactQuery getConnectionRequestsSentFactQuery) =>
                await getConnectionRequestsSentFactQuery.GetTotalConnectionRequestsForAdmin());
    }
}
