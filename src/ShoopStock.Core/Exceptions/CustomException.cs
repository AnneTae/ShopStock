﻿namespace ShoopStock.Core.Exceptions;

public class CustomException : Exception
{
    public CustomException(string? message) : base(message)
    {
    }
}
