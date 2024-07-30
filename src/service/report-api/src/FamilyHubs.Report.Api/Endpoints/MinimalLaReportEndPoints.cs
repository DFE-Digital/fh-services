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

public class MinimalLaReportEndPoints
{
    private const string AllowedRoles = $"{RoleGroups.LaManagerOrDualRole},{RoleGroups.VcsManagerOrDualRole}";

    public void RegisterLaReportEndPoints(WebApplication app)
    {
        app.MapGet("report/service-searches-past-7-days/organisation/{laOrgId:long}",
            [Authorize(Roles = AllowedRoles)] async (
                DateTime? date,
                ServiceType? serviceTypeId,
                long? laOrgId,
                IGetServiceSearchFactQuery getServiceSearchFactQuery,
                IValidator<LaSearchCountRequest> validator
            ) =>
            {
                LaSearchCountRequest request = new(date, serviceTypeId, laOrgId, 7);
                await validator.ValidateAndThrowAsync(request);
                return await getServiceSearchFactQuery.GetSearchCountForLa(request);
            });

        app.MapGet("report/service-searches-4-week-breakdown/organisation/{laOrgId:long}",
            [Authorize(Roles = AllowedRoles)] async (
                DateTime? date,
                ServiceType? serviceTypeId,
                long laOrgId,
                IGetFourWeekBreakdownQuery getFourWeekBreakdownQuery,
                IValidator<LaSearchBreakdownRequest> validator
            ) =>
            {
                LaSearchBreakdownRequest request = new(date, serviceTypeId, laOrgId);
                await validator.ValidateAndThrowAsync(request);
                return await getFourWeekBreakdownQuery.GetFourWeekBreakdownForLa(request);
            });

        app.MapGet("report/service-searches-total/organisation/{laOrgId:long}",
            [Authorize(Roles = AllowedRoles)] async (
                long? laOrgId,
                ServiceType? serviceTypeId,
                IGetServiceSearchFactQuery getServiceSearchFactQuery,
                IValidator<LaTotalSearchCountRequest> validator
            ) =>
            {
                LaTotalSearchCountRequest request = new(serviceTypeId, laOrgId);
                await validator.ValidateAndThrowAsync(request);
                return await getServiceSearchFactQuery.GetTotalSearchCountForLa(request);
            });

        app.MapGet("report/connection-requests-past-7-days/organisation/{LaOrgId:long}",
            [Authorize(Roles = AllowedRoles)]
            async (
                long? laOrgId,
                DateTime? date,
                IGetConnectionRequestsSentFactQuery getConnectionRequestsSentFactQuery,
                IValidator<LaConnectionRequestsRequest> validator) =>
            {
                LaConnectionRequestsRequest request = new(laOrgId, date, 7);
                await validator.ValidateAndThrowAsync(request);
                return await getConnectionRequestsSentFactQuery.GetConnectionRequestsForLa(request);
            });

        app.MapGet("report/connection-requests-4-week-breakdown/organisation/{lAOrgId:long}",
            [Authorize(Roles = AllowedRoles)]
            async (
                DateTime? date,
                long? laOrgId,
                IGetConnectionRequestsSentFactFourWeekBreakdownQuery getConnectionRequestsSentFactFourWeekBreakdownQuery,
                IValidator<LaConnectionRequestsBreakdownRequest> validator
            ) =>
            {
                LaConnectionRequestsBreakdownRequest request = new(date, laOrgId);
                await validator.ValidateAndThrowAsync(request);
                return await getConnectionRequestsSentFactFourWeekBreakdownQuery.GetFourWeekBreakdownForLa(request);
            });

        app.MapGet("report/connection-requests-total/organisation/{laOrgId:long}",
            [Authorize(Roles = AllowedRoles)]
            async (
                long? laOrgId,
                IGetConnectionRequestsSentFactQuery getConnectionRequestsSentFactQuery,
                IValidator<LaConnectionRequestsTotalRequest> validator) =>
            {
                LaConnectionRequestsTotalRequest request = new(laOrgId);
                await validator.ValidateAndThrowAsync(request);
                return await getConnectionRequestsSentFactQuery.GetTotalConnectionRequestsForLa(request);
            });
    }
}
