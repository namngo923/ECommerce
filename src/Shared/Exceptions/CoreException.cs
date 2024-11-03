﻿using System.Net;

namespace Shared.Exceptions;

public class CoreException : Exception
{
    public CoreException(string message, IEnumerable<string> errors,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
        : base(message)
    {
        ErrorMessages = errors;
        StatusCode = statusCode;
    }

    public CoreException(string message) : base(message)
    {
        ErrorMessages = [];
    }

    public IEnumerable<string> ErrorMessages { get; }

    public HttpStatusCode StatusCode { get; }
}