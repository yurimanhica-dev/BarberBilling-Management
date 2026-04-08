using BarberBilling.Application.UseCases.User;
using BarberBilling.Communication.Requests.Authentication.login;
using BarberBilling.Exceptions.CustomExceptions;
using FluentValidation;

namespace BarberBilling.Application.Validators;

public class LoginValidator : AbstractValidator<RequestLoginJson>
{
    public LoginValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("emailRequired")
            .EmailAddress().WithMessage("emailInvalid");

        RuleFor(r => r.Password)
            .SetValidator(new PasswordValidator<RequestLoginJson>());
    }

    public void ValidateInput(RequestLoginJson request)
    {
        var result = Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
