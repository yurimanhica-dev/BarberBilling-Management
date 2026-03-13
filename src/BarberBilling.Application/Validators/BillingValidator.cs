using BarberBilling.Application.UseCases.Billings;
using BarberBilling.Exceptions.Validation;
using FluentValidation;

namespace BarberBilling.Application.Validators;

public class BillingValidator : AbstractValidator<BillingInput>
{
    public BillingValidator()
    {
        RuleFor(b => b.BarberName)
        .NotEmpty().WithMessage("barberNameRequired");

        RuleFor(b => b.ClientName)
        .NotEmpty().WithMessage("clientNameRequired");

        RuleFor(b => b.ServiceName)
        .NotEmpty().WithMessage("serviceNameRequired");

        RuleFor(b => b.Amount)
        .GreaterThan(0).WithMessage("AmountGreaterThanZero");

        RuleFor(b => b.Date)
        .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow)).
        WithMessage("DateCannotBeFuture");

        RuleFor(b => b.PaymentMethod)
        .IsInEnum().WithMessage("PaymentMethodInvalid");

        RuleFor(b => b.Status)
        .IsInEnum().WithMessage("StatusInvalid");
    }

    public void ValidateInput(BillingInput input)
    {
        var result = Validate(input);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}