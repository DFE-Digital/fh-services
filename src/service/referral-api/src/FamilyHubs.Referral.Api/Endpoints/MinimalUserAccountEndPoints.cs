﻿using System.Net.Mime;
using FamilyHubs.Referral.Core.Commands.CreateUserAccount;
using FamilyHubs.Referral.Core.Commands.UpdateUserAccount;
using FamilyHubs.Referral.Core.Queries.GetUserAccounts;
using FamilyHubs.ReferralService.Shared.Dto;
using FamilyHubs.ReferralService.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FamilyHubs.Referral.Api.Endpoints;
public class MinimalUserAccountEndPoints
{ 
    public void RegisterUserAccountEndPoints(WebApplication app)
    {
        app.MapPost("api/useraccount", [Authorize(Policy = "ReferralUser")] async ([FromBody] UserAccountDto userAccount, CancellationToken cancellationToken, ISender _mediator) =>
        {
            CreateUserAccountCommand command = new(userAccount);
            var result = await _mediator.Send(command, cancellationToken);
            return result;

        }).WithMetadata(new SwaggerOperationAttribute("User Accounts", "Create Single User Account") { Tags = new[] { "User Accounts" } });

        app.MapPut("api/useraccount/{Id}", [Authorize(Policy = "ReferralUser")] async (long id, [FromBody] UserAccountDto userAccount, CancellationToken cancellationToken, ISender _mediator) =>
        {
            UpdateUserAccountCommand command = new(id, userAccount);
            var result = await _mediator.Send(command, cancellationToken);
            return result;

        }).WithMetadata(new SwaggerOperationAttribute("User Accounts", "Update User Accounts") { Tags = new[] { "User Accounts" } });

        app.MapPost("api/useraccounts", [Authorize(Policy = "ReferralUser")] async ([FromBody] List<UserAccountDto> userAccounts, CancellationToken cancellationToken, ISender _mediator) =>
        {
            CreateUserAccountsCommand command = new(userAccounts);
            var result = await _mediator.Send(command, cancellationToken);
            return result;

        }).WithMetadata(new SwaggerOperationAttribute("User Accounts", "Create User Accounts") { Tags = new[] { "User Accounts" } });

        app.MapPut("api/useraccounts", [Authorize(Policy = "ReferralUser")] async ([FromBody] List<UserAccountDto> userAccounts, CancellationToken cancellationToken, ISender _mediator) =>
        {
            UpdateUserAccountsCommand command = new(userAccounts);
            var result = await _mediator.Send(command, cancellationToken);
            return result;

        }).WithMetadata(new SwaggerOperationAttribute("User Accounts", "Update User Accounts") { Tags = new[] { "User Accounts" } });

        app.MapGet("api/useraccountsByOrganisationId/{organisationId}", [Authorize(Policy = "ReferralUser")] async (long organisationId, int? pageNumber, int? pageSize, CancellationToken cancellationToken, ISender _mediator) =>
        {
            GetUsersByOrganisationIdCommand command = new(organisationId, pageNumber, pageSize);
            var result = await _mediator.Send(command, cancellationToken);
            return result;

        })
            .WithMetadata(new SwaggerOperationAttribute("User Accounts", "Get User Accounts By Organisation Id") { Tags = new[] { "User Accounts" } })
            .Produces<PaginatedList<UserAccountDto>>(contentType: MediaTypeNames.Application.Json);
    }
}
