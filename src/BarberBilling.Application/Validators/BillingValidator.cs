using BarberBilling.Application.UseCases.Billings;
using BarberBilling.Communication.Requests.Billings;
using BarberBilling.Exceptions.ExceptionsBase;
using FluentValidation;

namespace BarberBilling.Application.Validators;

public class BillingValidator : AbstractValidator<BillingRequestJson>
{
    public BillingValidator()
    {
        // RuleFor(b => b.BarberName)
        // .NotEmpty().WithMessage("barberNameRequired");

        RuleFor(b => b.ClientName)
        .NotEmpty().WithMessage("clientNameRequired");

        RuleFor(b => b.ServiceName)
        .NotEmpty().WithMessage("serviceNameRequired");

        RuleFor(b => b.Amount)
        .GreaterThan(0).WithMessage("AmountGreaterThanZero");

        RuleFor(b => b.Date)
        .LessThanOrEqualTo(DateTime.UtcNow).
        WithMessage("DateCannotBeFuture");

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