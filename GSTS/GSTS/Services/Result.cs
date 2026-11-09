namespace GSTS.Services;

// The answer a service gives back to a form: did it work, and what message to DISPLAY.
// Id carries a new record's id when there is one (e.g. the new ticket).
public class Result
{
    public bool Success { get; }
    public string Message { get; }
    public int? Id { get; }

    private Result(bool success, string message, int? id)
    {
        Success = success;
        Message = message;
        Id = id;
    }

    public static Result Ok(string message, int? id = null) => new Result(true, message, id);
    public static Result Fail(string message) => new Result(false, message, null);
}
