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

public class MinimalOrgReportEndPoints
{
    private const string AllowedRoles = $"{RoleGroups.LaManagerOrDualRole},{RoleGroups.VcsManagerOrDualRole}";

    public void RegisterOrgReportEndPoints(WebApplication app)
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

        app.MapGet("report/connection-requests-past-7-days/organisation/{orgId:long}",
            [Authorize(Roles = AllowedRoles)]
            async (
                long? orgId,
                DateTime? date,
                IGetConnectionRequestsSentFactQuery getConnectionRequestsSentFactQuery,
                IValidator<OrgConnectionRequestsRequest> validator) =>
            {
                OrgConnectionRequestsRequest request = new(orgId, date, 7);
                await validator.ValidateAndThrowAsync(request);
                return await getConnectionRequestsSentFactQuery.GetConnectionRequestsForOrg(request);
            });

        app.MapGet("report/connection-requests-4-week-breakdown/organisation/{orgId:long}",
            [Authorize(Roles = AllowedRoles)]
            async (
                DateTime? date,
                long? orgId,
                IGetConnectionRequestsSentFactFourWeekBreakdownQuery getConnectionRequestsSentFactFourWeekBreakdownQuery,
                IValidator<OrgConnectionRequestsBreakdownRequest> validator
            ) =>
            {
                OrgConnectionRequestsBreakdownRequest request = new(date, orgId);
                await validator.ValidateAndThrowAsync(request);
                return await getConnectionRequestsSentFactFourWeekBreakdownQuery.GetFourWeekBreakdownForOrg(request);
            });

        app.MapGet("report/connection-requests-total/organisation/{orgId:long}",
            [Authorize(Roles = AllowedRoles)]
            async (
                long? orgId,
                IGetConnectionRequestsSentFactQuery getConnectionRequestsSentFactQuery,
                IValidator<OrgConnectionRequestsTotalRequest> validator) =>
            {
                OrgConnectionRequestsTotalRequest request = new(orgId);
                await validator.ValidateAndThrowAsync(request);
                return await getConnectionRequestsSentFactQuery.GetTotalConnectionRequestsForOrg(request);
            });
    }
}
