﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyHubs.Idams.Maintenance.Core.Models;

public enum ErrorId
{
    SupportDetails_Invalid,
    //todo: could have generic TellTheService enums, and then have add the different error messages to the interface?
    // ^^ but that wouldn't work well if we want to centralise all error messages in config
    // ok now we have the page as part of the error state
    // centralised error messages by page and error will simplify the models, where we could have 1 model for the telltheservice and singletextboxpage
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
    ContactByPhone_InvalidTelephoneNumber
}