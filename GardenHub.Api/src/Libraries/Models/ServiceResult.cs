using System.Collections.Generic;

namespace Models;

public class ServiceResult<T>
{
    public ServiceResult()
    {
    }
    public ServiceResult(T data, string message = null!)
    {
        Message = message;
        Data = data;
    }
    public ServiceResult(string message)
    {
        Message = message;
    }

    public T? Data { get; set; }
    public bool Successful { get; set; } = true;
    public string Message { get; set; } = string.Empty;
    public List<string> Errors = new();
}
