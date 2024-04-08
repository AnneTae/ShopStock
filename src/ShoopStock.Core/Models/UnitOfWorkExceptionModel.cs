namespace ShoopStock.Core.Models;

public class UnitOfWorkExceptionModel
{
    public bool Succeeded { get; set; }

    public string? Message { get; set; }
}
