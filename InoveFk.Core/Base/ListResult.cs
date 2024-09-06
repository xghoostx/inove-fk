namespace InoveFk.Core.Base;

public class ListResult<T>
{
    public uint Count { get; set; }
    public uint Skip { get; set; }
    public uint Take { get; set; }
    public IEnumerable<T>? Data { get; set; }
}