namespace FamilyHubs.SharedKernel;

public class Result<T>
{
    public T? Data { get; }
    public bool IsSuccess { get; }
    public string? ErrorMessage { get; }

    private Result(T data)
    {
        Data = data;
        IsSuccess = true;
        ErrorMessage = null;
    }

    private Result(string errorMessage)
    {
        Data = default;
        IsSuccess = false;
        ErrorMessage = errorMessage;
    }

    public static Result<T> Success(T data) => new(data);
    public static Result<T> Failure(string errorMessage) => new(errorMessage);
}