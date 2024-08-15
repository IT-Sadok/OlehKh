public class Result
{
    public bool Success { get; private set; }
    public string? Message { get; private set; }

    public void SetResult(bool success, string message)
    {
        Success = success;
        Message = message;
    }
}