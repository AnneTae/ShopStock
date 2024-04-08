namespace ShoopStock.Core.Exceptions;

public class InvalidAssignException : Exception
{
    public InvalidAssignException(string? message) : base(message)
    {
    }
}
