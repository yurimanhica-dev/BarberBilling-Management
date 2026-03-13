using System.Net;
using BarberBilling.Exceptions.Base;

namespace BarberBilling.Exceptions.Validation;

public class ErrorOnValidationException : BarberBillingException
{
    private readonly List<string> _errorKeys;

    public override int StatusCode => (int)HttpStatusCode.BadRequest;

    public ErrorOnValidationException(List<string> errorKeys) : base(string.Empty)
    {
        _errorKeys = errorKeys;
    }

    public override List<string> GetErrors()
    {
        return _errorKeys;
    }
}