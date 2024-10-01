
namespace FamilyHubs.Idam.Core.Exceptions;

public class NotFoundException : IdamsException
{
    public NotFoundException(string message) : base(message)
    {
        Title = "Not Found";
        HttpStatusCode = 404;
        ErrorCode = ExceptionCodes.NotFoundException;
    }
}