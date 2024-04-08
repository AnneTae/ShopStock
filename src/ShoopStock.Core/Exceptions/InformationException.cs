namespace ShoopStock.Core.Identity;

public class InformationException : Exception
{
    public IEnumerable<string> Errors { get; set; } = new List<string>();

    public InformationException(IEnumerable<string> errors)
    {
        Errors = errors;
    }
}
