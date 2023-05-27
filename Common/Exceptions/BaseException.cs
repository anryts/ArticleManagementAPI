using System.Net;
using Common.Enums;

namespace Common.Exceptions;

public class BaseException : Exception
{
    public ErrorCode ErrorCode { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public BaseException(string message) : base(message) {}
}