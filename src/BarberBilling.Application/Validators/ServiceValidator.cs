using BarberBilling.Communication.Requests.Services;
using BarberBilling.Exceptions.CustomExceptions;
using FluentValidation;

namespace BarberBilling.Application.Validators;

public class ServiceValidator : AbstractValidator<RequestServiceJson>
{
    public ServiceValidator()
    {
        RuleFor(s => s.Services)
            .IsInEnum().WithMessage("serviceInvalid");

        RuleFor(s => s.Price)
            .GreaterThan(0).WithMessage("priceMustBeGreaterThanZero");

        RuleFor(s => s.Category)
            .IsInEnum().WithMessage("categoryInvalid");
    }

    public void ValidateInput(RequestServiceJson request)
    {
        var result = Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
