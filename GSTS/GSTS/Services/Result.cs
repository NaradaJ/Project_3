namespace GSTS.Services;

// The answer a service gives back to a form: did it work, and what message to DISPLAY.
// The messages are the ones written in the report's pseudocode.
public class Result
{
    public bool Success { get; }
    public string Message { get; }

    private Result(bool success, string message)
    {
        Success = success;
        Message = message;
    }

    public static Result Ok(string message) => new Result(true, message);
    public static Result Fail(string message) => new Result(false, message);
}
