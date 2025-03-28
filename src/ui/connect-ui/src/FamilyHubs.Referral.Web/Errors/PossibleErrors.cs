﻿using FamilyHubs.Referral.Core.Models;
using FamilyHubs.SharedKernel.Razor.ErrorNext;
using System.Collections.Immutable;

namespace FamilyHubs.Referral.Web.Errors;

public static class PossibleErrors
{
    public static readonly ImmutableDictionary<int, PossibleError> All = ImmutableDictionary
        .Create<int, PossibleError>()
        .Add(ErrorId.SupportDetails_Invalid, "Enter a full name")
        .Add(ErrorId.Email_NotValid, "Enter an email address in the correct format, like name@example.com")
        .Add(ErrorId.Consent_NoConsentSelected, "Select whether you have permission to share details")
        .Add(ErrorId.SharePrivacy_NoSelection, "Select whether you have shared our privacy statement")
        .Add(ErrorId.ContactByPhone_NoContactSelected, "Select how this service can contact you")
        .Add(ErrorId.ContactByPhone_NoTelephoneNumber, "Enter a UK telephone number")
        .Add(ErrorId.ContactByPhone_InvalidTelephoneNumber, "Enter a telephone number, like 01632 960 001, 07700 900 982 or +44 808 157 0192")
        .Add(ErrorId.WhySupport_NothingEntered, "Enter details about the people who need support")
        .Add(ErrorId.WhySupport_TooLong, "Reason for the connection request must be 500 characters or less")
        .Add(ErrorId.ContactMethods_NothingEntered, "Enter details about the people who need support")
        .Add(ErrorId.ContactMethods_TooLong, "How the service can engage with the family must be 500 characters or less")
        .Add(ErrorId.ContactDetails_NoContactMethodsSelected, "Select a contact method")
        .Add(ErrorId.ChangeName_EnterAName, "Enter a name")
        .Add(ErrorId.VcsRequestDetails_SelectAResponse, "You must select a response")
        .Add(ErrorId.VcsRequestDetails_EnterReasonForDeclining, "Enter a reason for declining")
        .Add(ErrorId.VcsRequestDetails_ReasonForDecliningTooLong, "Reason for declining must be 500 characters or less")
        .Add(ErrorId.NoPostcode, "You need to enter a postcode")
        .Add(ErrorId.InvalidPostcode, "Your postcode is not recognised - try another one")
        .Add(ErrorId.PostcodeNotFound, "You need to enter a valid postcode")
        ;
}