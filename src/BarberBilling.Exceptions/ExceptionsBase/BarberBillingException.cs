namespace BarberBilling.Exceptions.ExceptionsBase;

public abstract class BarberBillingException : SystemException
{
    public virtual string ErrorKey { get; } = string.Empty;
    protected BarberBillingException(string message) : base(message)
    {
    }
    public abstract int StatusCode { get; }
    public virtual List<string> GetErrors()
    {
        return [ErrorKey];
    }
}