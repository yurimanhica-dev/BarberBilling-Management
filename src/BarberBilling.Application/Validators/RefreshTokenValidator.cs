using BarberBilling.Communication.Requests.Authentication.RefreshToken;
using BarberBilling.Exceptions.CustomExceptions;
using FluentValidation;

namespace BarberBilling.Application.Validators;

public class RefreshTokenValidator : AbstractValidator<RequestRefreshTokenJson>
{
    public RefreshTokenValidator()
    {
        RuleFor(r => r.RefreshToken)
            .NotEmpty().WithMessage("refreshTokenRequired");
    }

    public void ValidateInput(RequestRefreshTokenJson request)
    {
        var result = Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
