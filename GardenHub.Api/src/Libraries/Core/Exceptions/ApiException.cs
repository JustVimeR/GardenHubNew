using System;
using System.Globalization;

namespace Core.Exceptions;

public class ApiException : Exception
{
    public ApiException() : base() { }

    public ApiException(string message) : base(message) { }
    
    public ApiException(string message, params object[] args)
        : base(string.Format(CultureInfo.CurrentCulture, message, args))
    {
    }

    public ApiException(int statusCode, string message) : this(message)
    {
        this.StatusCode = statusCode;
    }

    public ApiException(int statusCode, string message, params object[] args)
        : this(message, args)
    {
        this.StatusCode = statusCode;
    }

    public int StatusCode { get; set; }
}
