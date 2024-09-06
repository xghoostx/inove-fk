namespace InoveFk.Core.Base;

public class Result<T>
{
    public Result(bool success, T? data)
    {
        Success = success;
        Data = data;
    }

    public Result() { }

    public bool Success { get; set; }
    public T? Data { get; set; }
}
