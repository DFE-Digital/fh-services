﻿
using System.Diagnostics.CodeAnalysis;

namespace FamilyHubs.Idams.Maintenance.Core.Exceptions;

[ExcludeFromCodeCoverage]
public class AuthorisationException : IdamsException
{
    public AuthorisationException(string message, int statusCode = 401, string errorCode = ExceptionCodes.GenericAuthorizationException):base(message)
    {
        Title = "Authorization Error";
        HttpStatusCode = statusCode;
        ErrorCode = errorCode;
    }
}
