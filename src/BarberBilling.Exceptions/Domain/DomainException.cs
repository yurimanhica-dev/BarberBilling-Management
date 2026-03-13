using System.Net;
using BarberBilling.Exceptions.Base;

namespace BarberBilling.Exceptions.Domain;

public class DomainException : BarberBillingException
{
    public override string ErrorKey { get; }

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public DomainException(string errorKey) : base(errorKey)
    {
        ErrorKey = errorKey;
    }
}