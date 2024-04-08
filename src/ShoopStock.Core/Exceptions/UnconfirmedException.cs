namespace ShoopStock.Core.Exceptions;

public class UnconfirmedException : Exception
{
    public UnconfirmedException(string? message) : base(message)
    {
    }
}