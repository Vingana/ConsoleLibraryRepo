namespace ConsoleLibrary;

public sealed record OperationResult(bool Success, string Message = "", string ErrorCode = "")
{
    public static OperationResult Ok(string message = "") => new(true, message);
    public static OperationResult Fail(string message, string errorCode = "") => new(false, message, errorCode);
}