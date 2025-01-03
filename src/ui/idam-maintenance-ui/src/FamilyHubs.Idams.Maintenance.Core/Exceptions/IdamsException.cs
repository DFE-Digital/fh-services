
using System.Diagnostics.CodeAnalysis;

namespace FamilyHubs.Idams.Maintenance.Core.Exceptions;

[ExcludeFromCodeCoverage]
public class IdamsException : Exception
{
    public string Title { get; set; } = "Server Error";
    public string ErrorCode { get; set; } = ExceptionCodes.UnhandledException;
    public int HttpStatusCode { get; set; } = 500;
        
    public IdamsException(string message) :base(message)
    {

    }
}

