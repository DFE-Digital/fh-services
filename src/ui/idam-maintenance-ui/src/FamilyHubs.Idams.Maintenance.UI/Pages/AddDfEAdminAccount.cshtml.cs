using FamilyHubs.Idams.Maintenance.Core.Services;
using FamilyHubs.Idams.Maintenance.UI.Pages.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace FamilyHubs.Idams.Maintenance.UI.Pages;

public class AddDfEAdminAccountModel : HeaderPageModel
{
    [Required]
    [BindProperty]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(254, MinimumLength = 3)] // the EmailAddress attribute allows emails that are too long, so we have this too
    [BindProperty]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    public string? PhoneNumber { get; set; }
    public record FormPropertyError(string Property, string ErrorMessage);
    public FormPropertyError[]? FormPropertyErrors { get; set; }

    public bool ValidationValid { get; private set; } = true;
    private readonly IIdamService _idamService;


    public AddDfEAdminAccountModel(IIdamService idamService)
    {
        _idamService = idamService;
    }

    public async Task<IActionResult> OnPost()
    {
        if (!string.IsNullOrWhiteSpace(PhoneNumber) && !Regex.IsMatch(PhoneNumber, @"^[A-Za-z0-9]*$"))
        {
            ModelState.AddModelError("PhoneNumber", "Please Enter a valid phone number");
            ValidationValid = false;
        }

        if (string.IsNullOrWhiteSpace(Email) || !MailAddress.TryCreate(Email, out _))
        {
            ModelState.AddModelError("Email", "Please Enter a valid email address");
            ValidationValid = false;
        }

        if (string.IsNullOrWhiteSpace(Name))
        {
            ModelState.AddModelError("Name", "Please Enter a valid name");
            ValidationValid = false;
        }
        
        if (!ModelState.IsValid || !ValidationValid)
        {
            FormPropertyErrors = GetErrors().ToArray();
            return Page();
        }

        var result = await  _idamService.AddNewDfeAccount(Name, Email.ToLower(), PhoneNumber);
        if (result != Email.ToLower())
        {
            ValidationValid = false;
            ModelState.AddModelError("Name", "Failed to create DfE Admin");
            return Page();
        }

        long accountId = await _idamService.GetAccountIdByEmail(result);

        return RedirectToPage("AddDfEAccountConfirmation", new { accountId });
    }

    private IEnumerable<FormPropertyError> GetErrors()
    {
        foreach (var modelError in ModelState)
        {
            if (modelError.Value is { ValidationState: ModelValidationState.Invalid, Errors.Count: > 0 })
            {
                yield return new FormPropertyError(modelError.Key, modelError.Value.Errors[0].ErrorMessage);
            }
        }
    }
}
