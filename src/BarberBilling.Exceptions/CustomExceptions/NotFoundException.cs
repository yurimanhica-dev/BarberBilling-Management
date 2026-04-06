using System.Net;

namespace BarberBilling.Exceptions.CustomExceptions;

public class NotFoundException : BarberBillingException
{
    public override string ErrorKey { get; }

    public override int StatusCode => (int)HttpStatusCode.NotFound;

    public NotFoundException(string errorKey) : base(errorKey)
    {
        ErrorKey = errorKey;
    }
}