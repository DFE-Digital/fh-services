
namespace FamilyHubs.Idams.Maintenance.Core.Exceptions;

public class AlreadyExistsException : IdamsException
{
    public AlreadyExistsException(string message):base(message)
    {
        Title = "Already Exists";
        HttpStatusCode = 400;
        ErrorCode = ExceptionCodes.AlreadyExistsException;
    }
}
