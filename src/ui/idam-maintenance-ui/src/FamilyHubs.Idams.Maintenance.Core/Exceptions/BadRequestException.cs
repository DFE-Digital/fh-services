
namespace FamilyHubs.Idams.Maintenance.Core.Exceptions;

public class BadRequestException : IdamsException
{
    public BadRequestException(string message) : base(message)
    {
        Title = "Bad Request";
        HttpStatusCode = 400;
        ErrorCode = ExceptionCodes.BadRequest;
    }
}
