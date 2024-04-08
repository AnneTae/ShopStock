namespace ShoopStock.Api.Models.Errors;

public class ErrorResponseModel
{
    public string[] Data { get; set; } = null!;
    public IEnumerable<string> ErrorMessages { get; set; } = null!;

    public ErrorResponseModel()
    {
    }

    public ErrorResponseModel(string errorMessage)
    {
        Data = Array.Empty<string>();
        ErrorMessages = new List<string> { errorMessage };
    }

    public ErrorResponseModel(IEnumerable<string> errorMessages)
    {
        Data = Array.Empty<string>();
        ErrorMessages = errorMessages;
    }
}
