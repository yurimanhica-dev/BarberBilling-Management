using BarberBilling.Communication.Requests.Billings;
using BarberBilling.Exceptions.CustomExceptions;
using FluentValidation;

namespace BarberBilling.Application.Validators;

public class BillingValidator : AbstractValidator<BillingRequestJson>
{
    public BillingValidator()
    {
        RuleFor(b => b.ClientIdentifier)
            .NotEmpty().WithMessage("clientRequired");

        RuleFor(b => b.ServiceIds)
            .NotEmpty().WithMessage("servicesRequired")
            .Must(s => s.Count > 0).WithMessage("atLeastOneServiceRequired");

        RuleFor(b => b.Date)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("DateCannotBeFuture");

        RuleFor(b => b.PaymentMethod)
            .IsInEnum().WithMessage("PaymentMethodInvalid");

        RuleFor(b => b.Status)
            .IsInEnum().WithMessage("StatusInvalid");
    }

    public void ValidateInput(BillingRequestJson request)
    {
        var result = Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}