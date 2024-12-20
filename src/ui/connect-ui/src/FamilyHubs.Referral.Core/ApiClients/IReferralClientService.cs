using FamilyHubs.ReferralService.Shared.Models;
using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.ReferralService.Shared.Dto.CreateUpdate;
using FamilyHubs.ReferralService.Shared.Enums;

namespace FamilyHubs.Referral.Core.ApiClients;

public enum ReferralStatus
{
    New = 1,
    Opened,
    Accepted,
    Declined
}

public interface IReferralClientService
{
    Task<ReferralResponse> CreateReferral(CreateReferralDto createReferralDto, CancellationToken cancellationToken = default);
}

public interface IReferralDashboardClientService
{
    Task<PaginatedList<ReferralDto>> GetRequestsForConnectionByOrganisationId(string organisationId, ReferralOrderBy? orderBy, bool? isAscending, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<PaginatedList<ReferralDto>> GetRequestsByLaProfessional(string accountId, ReferralOrderBy? orderBy, bool? isAscending, int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
    Task<ReferralDto> GetReferralById(long referralId, CancellationToken cancellationToken = default);
    // don't support cancellation: we don't want to cancel the update if the cancellation token is cancelled
    Task<string> UpdateReferralStatus(long referralId, ReferralStatus referralStatus, string? reason = null);
}
