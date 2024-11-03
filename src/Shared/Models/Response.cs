namespace SPSVN.Shared.Models;

public class Response<T>
{
    public bool Error { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public int ResultCode { get; set; } = 200;
}

public class Response
{
    public bool Error { get; set; }
    public string? Message { get; set; }
    public object? Data { get; set; }
    public int ResultCode { get; set; } = 200;
}