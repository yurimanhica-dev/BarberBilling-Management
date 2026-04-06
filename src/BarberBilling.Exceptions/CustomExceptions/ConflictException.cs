using System.Net;

namespace BarberBilling.Exceptions.CustomExceptions;
public class ConflictException : BarberBillingException
{
    public override string ErrorKey { get; }
    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ConflictException(string errorKey) : base(errorKey)
    {
        ErrorKey = errorKey;
    }
}

