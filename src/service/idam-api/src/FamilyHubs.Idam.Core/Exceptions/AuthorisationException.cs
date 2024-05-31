﻿using System.Runtime.Serialization;

namespace FamilyHubs.Idam.Core.Exceptions;

[Serializable]
public class AuthorisationException : IdamsException
{
    public AuthorisationException(string message, int statusCode = 401, string errorCode = ExceptionCodes.GenericAuthorizationException):base(message)
    {
        Title = "Authorization Error";
        HttpStatusCode = statusCode;
        ErrorCode = errorCode;
    }

    protected AuthorisationException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}
