namespace FamilyHubs.Referral.Core.Models;

//todo: error enum per error / page / journey??
//could have every error message in config. would we want to do that?
public enum ErrorId
{
    SupportDetails_Invalid,
    WhySupport_NothingEntered,
    WhySupport_TooLong,
    ContactMethods_NothingEntered,
    ContactMethods_TooLong,
    ContactDetails_NoContactMethodsSelected,
    SharePrivacy_NoSelection,
    Consent_NoConsentSelected,
    Email_NotValid,
    ContactByPhone_NoContactSelected,
    ContactByPhone_NoTelephoneNumber,
    ContactByPhone_InvalidTelephoneNumber,
    ChangeName_EnterAName,
    NoPostcode,
    InvalidPostcode,
    PostcodeNotFound,
    Letter_AddressLine1,
    Letter_TownOrCity,
    Letter_Postcode,
}

public enum AdHocErrorId
{
    Error1
}
