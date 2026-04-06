using System.Net;

namespace BarberBilling.Exceptions.CustomExceptions;

public class DomainException : BarberBillingException
{
    public override string ErrorKey { get; }

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public DomainException(string errorKey) : base(errorKey)
    {
        ErrorKey = errorKey;
    }
}