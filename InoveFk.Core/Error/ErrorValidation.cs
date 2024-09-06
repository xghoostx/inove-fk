namespace InoveFk.Core.Error;

public class ErrorValidation
{
    public bool Invalid { get; set; }
    public IEnumerable<ErrorResponse> Errors { get; set; }
}
