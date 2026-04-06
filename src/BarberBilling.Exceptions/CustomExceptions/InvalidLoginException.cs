using System.Net;

namespace BarberBilling.Exceptions.CustomExceptions;

public class InvalidLoginException : BarberBillingException
{
    public override string ErrorKey { get; }
    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public InvalidLoginException(string errorKey) : base(errorKey)
    {
        ErrorKey = errorKey;
    }
}