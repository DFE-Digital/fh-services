using System.Net;
using FamilyHubs.Notification.Api.Client;
using FamilyHubs.Notification.Api.Client.Templates;
using FamilyHubs.Referral.Core.ApiClients;
using FamilyHubs.Referral.Core.Models;
using FamilyHubs.Referral.Infrastructure.Notifications;
using FamilyHubs.Referral.Web.Errors;
using FamilyHubs.Referral.Web.Pages.Shared;
using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.SharedKernel.Identity;
using FamilyHubs.SharedKernel.Razor.ErrorNext;
using FamilyHubs.SharedKernel.Razor.FamilyHubsUi.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FamilyHubs.Referral.Web.Pages.Referrals.Vcs;

public enum UserAction
{
    AcceptDecline,
    ReturnLater
}

public enum AcceptDecline
{
    Accepted,
    Declined
}

[Authorize(Roles = $"{RoleGroups.VcsProfessionalOrDualRole}")]
public class VcsRequestDetailsPageModel : HeaderPageModel
{
    private readonly IReferralDashboardClientService _referralClientService;
    private readonly INotifications _notifications;
    private readonly INotificationTemplates<NotificationType> _notificationTemplates;
    private readonly ILogger<VcsRequestDetailsPageModel> _logger;
    public ReferralDto Referral { get; set; } = default!;

    [BindProperty]
    public string? ReasonForRejection { get; set; }

    [BindProperty]
    public AcceptDecline? AcceptOrDecline { get; set; }

    public IErrorState Errors { get; private set; }

    public VcsRequestDetailsPageModel(
        IReferralDashboardClientService referralClientService,
        INotifications notifications,
        INotificationTemplates<NotificationType> notificationTemplates,
        IOptions<FamilyHubsUiOptions> familyHubsUiOptions,
        ILogger<VcsRequestDetailsPageModel> logger) : base(false, true)
    {
        _referralClientService = referralClientService;
        _notifications = notifications;
        _notificationTemplates = notificationTemplates;
        _logger = logger;
        //todo: do something so doesn't have to be fully qualified
        Errors = ErrorState.Empty;
    }

    public async Task<IActionResult> OnGet(int id, IEnumerable<ErrorId> errors)
    {
        Errors = ErrorState.Create(PossibleErrors.All, errors.ToArray());

        // if the user enters a reason for declining that's too long, then refreshes the page with the corresponding error message on, they'll lose their reason. quite an edge case though, and the site will still work, they'll just have to enter a shorter reason from scratch
        ReasonForRejection  = TempData["ReasonForDeclining"] as string;
        //todo: check errorIds are valid?

        try
        {
            Referral = await _referralClientService.GetReferralById(id);
        }
        catch (ReferralClientServiceException ex)
        {
            // user has changed the id in the url to see a referral they shouldn't have access to
            if (ex.StatusCode == HttpStatusCode.Forbidden)
            {
                return Redirect("/Error/403");
            }
            throw;
        }
        return Page();
    }

    //todo: component for character count (especially as there are some gotchas with line endings)
    public async Task<IActionResult> OnPost(UserAction userAction, int id)
    {
        if (userAction == UserAction.ReturnLater)
        {
            await _referralClientService.UpdateReferralStatus(id, ReferralStatus.Opened);
            return RedirectToPage("Dashboard");
        }

        ReferralStatus? newStatus = null;
        string? redirectTo = null;
        string? reason = null;
        var errors = new List<ErrorId>();

        switch (AcceptOrDecline)
        {
            case AcceptDecline.Accepted:
                newStatus = ReferralStatus.Accepted;
                redirectTo = "RequestAccepted";
                break;
            case AcceptDecline.Declined:
                if (string.IsNullOrEmpty(ReasonForRejection))
                {
                    errors.Add(ErrorId.VcsRequestDetails_EnterReasonForDeclining);
                }
                // workaround the front end counting line endings as 1 chars (\n) as per HTML spec,
                // and the http transport/.net/windows using 2 chars for line ends (\r\n)
                else if (ReasonForRejection.Replace("\r", "").Length > 500)
                {
                    errors.Add(ErrorId.VcsRequestDetails_ReasonForDecliningTooLong);
                    // truncate at some large value, although it gets stored in cookie(s), so large values will only affect the user that sends them
                    TempData["ReasonForDeclining"] = ReasonForRejection[..Math.Min(ReasonForRejection.Length, 4000)];
                }
                else
                {
                    newStatus = ReferralStatus.Declined;
                    redirectTo = "RequestDeclined";
                    reason = ReasonForRejection;
                }
                break;
            default:
                errors.Add(ErrorId.VcsRequestDetails_SelectAResponse);
                break;
        }

        if (errors.Any())
        {
            return RedirectToPage("RequestDetails", routeValues: new { id, errors });
        }

        if (newStatus == null)
        {
            throw new InvalidOperationException("Unexpected values in posted form");
        }

        await _referralClientService.UpdateReferralStatus(id, newStatus.Value, reason);

        try
        {
            Referral = await _referralClientService.GetReferralById(id);
        }
        catch (ReferralClientServiceException ex)
        {
            // user has changed the id in the url to see a referral they shouldn't have access to
            if (ex.StatusCode == HttpStatusCode.Forbidden)
            {
                return Redirect("/Error/403");
            }
            throw;
        }

        await TrySendNotificationEmails(Referral.ReferralUserAccountDto.EmailAddress, newStatus, Referral.ReferralServiceDto.Name ?? "", id);

        return RedirectToPage(redirectTo, routeValues: new { id });
    }

    private async Task TrySendNotificationEmails(
        string emailAddress,
        ReferralStatus? newStatus,
        string serviceName,
        int requestNumber)
    {
        // we _could_ currently use INotificationTemplates<ReferralStatus> directly, but this is more future proof
        NotificationType notificationType = newStatus switch
        {
            ReferralStatus.Accepted => NotificationType.ProfessionalAcceptedRequest,
            ReferralStatus.Declined => NotificationType.ProfessionalDeclinedRequest,
            _ => throw new ArgumentOutOfRangeException(nameof(newStatus), $"Unexpected value {newStatus}")
        };

        try
        {
            await SendNotificationEmails(emailAddress, notificationType, requestNumber, serviceName);
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Unable to send {NotificationType} email for request {RequestNumber}", notificationType, requestNumber);
        }
    }

    private async Task SendNotificationEmails(
        string emailAddress,
        NotificationType notificationType,
        int requestNumber,
        string serviceName)
    {
        var viewConnectionRequestUrl = Url.PageLink("RequestDetails", values: new { id = requestNumber }) ?? "";

        var emailTokens = new Dictionary<string, string>
        {
            { "RequestNumber", requestNumber.ToString("X6") },
            { "ServiceName", serviceName },
            { "ViewConnectionRequestUrl", viewConnectionRequestUrl}
        };

        string templateId = _notificationTemplates.GetTemplateId(notificationType);

        //todo: change api to accept IEnumerable and dynamic dictionary
        await _notifications.SendEmailsAsync(new List<string> { emailAddress }, templateId, emailTokens);
    }
}
