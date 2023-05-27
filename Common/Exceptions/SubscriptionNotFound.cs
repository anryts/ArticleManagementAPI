using System.Net;
using Common.Enums;

namespace Common.Exceptions;

public class SubscriptionNotFound : BaseException
{
    public SubscriptionNotFound(string message) : base(message)
    {
        StatusCode = HttpStatusCode.NotFound;
        ErrorCode = ErrorCode.SubscriptionNotFound;
    }
}