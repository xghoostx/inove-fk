namespace InoveFk.Core.Error;

public class ErrorResponse
{
    public string Key { get; }
    public string Message { get; }
    public string StatusCode { get; private set; }
    public ErrorResponse(string key, string message, string statusCode)
    {
        Key = key;
        Message = message;
        StatusCode = statusCode;
    }
}
