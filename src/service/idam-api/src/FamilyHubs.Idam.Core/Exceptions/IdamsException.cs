
namespace FamilyHubs.Idam.Core.Exceptions;

public class IdamsException(string message) : Exception(message)
{
    public string Title { get; set; } = "Server Error";
    public string ErrorCode { get; set; } = ExceptionCodes.UnhandledException;
    public int HttpStatusCode { get; set; } = 500;
}

