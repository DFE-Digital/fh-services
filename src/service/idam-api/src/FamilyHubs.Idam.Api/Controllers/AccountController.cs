using FamilyHubs.Idam.Core.Commands.Add;
using FamilyHubs.Idam.Core.Commands.Delete;
using FamilyHubs.Idam.Core.Commands.Update;
using FamilyHubs.Idam.Core.Exceptions;
using FamilyHubs.Idam.Core.Models;
using FamilyHubs.Idam.Core.Queries.GetAccount;
using FamilyHubs.Idam.Core.Queries.GetAccounts;
using FamilyHubs.Idam.Core.Queries.GetVcsUserEmails;
using FamilyHubs.Idam.Data.Entities;
using FamilyHubs.SharedKernel.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FamilyHubs.Idam.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = $"{RoleTypes.DfeAdmin},{RoleTypes.LaManager},{RoleTypes.LaDualRole}")]
        public async Task<string> AddAccount([FromBody] AddAccountCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            HttpContext.Response.StatusCode = (int)HttpStatusCode.Created;
            HttpContext.Response.Headers.Add("Location", $"Api/Account?email={result}");
            return "Account Created";
        }

        [HttpGet]
        public async Task<Account?> GetAccount(string email, CancellationToken cancellationToken)
        {
            var request = new GetAccountCommand { Email = email };
            var result = await _mediator.Send(request, cancellationToken);
            if(result == null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            }

            return result;
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<Account?> GetAccountById(long id, CancellationToken cancellationToken)
        {
            var request = new GetAccountByIdCommand { Id = id };
            var result = await _mediator.Send(request, cancellationToken);
            if (result == null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            }

            return result;
        }

        [HttpGet]
        [Route("List")]
        [Authorize(Roles = $"{RoleTypes.DfeAdmin},{RoleTypes.LaManager},{RoleTypes.LaDualRole}")]
        public async Task<PaginatedList<Account>?> GetAccounts(
            long? organisationId, 
            int? pageNumber, 
            int? pageSize, 
            string? userName, 
            string? email, 
            string? organisationName, 
            bool? isLaUser, 
            bool? isVcsUser, 
            string? sortBy,
            CancellationToken cancellationToken)
        {

            var request = new GetAccountsCommand(organisationId, pageSize, pageNumber, userName, email, organisationName, isLaUser, isVcsUser, sortBy);
            var result = await _mediator.Send(request, cancellationToken);
            if (result == null)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.NoContent;
            }

            return result;
        }

        [HttpGet]
        [Route("VcsProfessionalList")]
        [Authorize(Roles = RoleGroups.LaProfessionalOrDualRole)]
        public async Task<IEnumerable<Account>> GetVcsAccounts(
            long? organisationId,
            CancellationToken cancellationToken)
        {
            var request = new GetVcsAccountsCommand(organisationId);
            var result = await _mediator.Send(request, cancellationToken);

            return result;
        }

        [HttpPut]
        [Authorize(Roles = $"{RoleTypes.DfeAdmin},{RoleTypes.LaManager},{RoleTypes.LaDualRole}")]
        public async Task UpdateAccount([FromBody] UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            await _mediator.Send(request, cancellationToken);
        }

        [HttpPut]
        [Route("self-service")]
        [Authorize]
        public async Task UpdateAccountSelfService([FromBody] UpdateAccountSelfServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _mediator.Send(request, cancellationToken);
            }
            catch (AuthorisationException ex)
            {
                HttpContext.Response.StatusCode = ex.HttpStatusCode;
                HttpContext.Response.Headers.Add("WWW-Authenticate", ex.Message);
            }
        }

        [HttpDelete]        
        [Authorize(Roles = $"{RoleTypes.DfeAdmin},{RoleTypes.LaManager},{RoleTypes.LaDualRole}")]
        public async Task<bool> DeleteAccount([FromBody] DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }

        [HttpDelete]        
        [Route("DeleteOrganisationAccounts")]
        [Authorize(Roles = $"{RoleTypes.DfeAdmin},{RoleTypes.LaManager},{RoleTypes.LaDualRole}")]
        public async Task<bool> DeleteOrganisationAccounts([FromBody] DeleteOrganisationAccountsCommand request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(request, cancellationToken);
            return result;
        }
    }
}