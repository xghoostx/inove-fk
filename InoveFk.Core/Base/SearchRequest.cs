namespace InoveFk.Core.Base;

public abstract class SearchRequest
{
    public string? Id { get; set; }
    public uint Skip { get; set; }
    public uint Take { get; set; }
}
