﻿
namespace FamilyHubs.Idam.Core.Exceptions;

public class AlreadyExistsException : IdamsException
{
    public AlreadyExistsException(string message) : base(message)
    {
        Title = "Already Exists";
        HttpStatusCode = 400;
        ErrorCode = ExceptionCodes.AlreadyExistsException;
    }
}
