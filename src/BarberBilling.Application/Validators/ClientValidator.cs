using BarberBilling.Application.UseCases.User;
using BarberBilling.Communication.Requests.Authentication.RegisterClient;
using FluentValidation;

namespace BarberBilling.Application.Validators;
public class ClientValidator : AbstractValidator<RequestRegisterClientJson>
{
    public ClientValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("nameRequired")
            .MinimumLength(3).WithMessage("nameTooShort")
            .MaximumLength(100).WithMessage("nameTooLong");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("emailRequired")
            .EmailAddress().WithMessage("emailInvalid")
            .MaximumLength(100).WithMessage("emailTooLong");

        RuleFor(u => u.Password)
            .SetValidator(new PasswordValidator<RequestRegisterClientJson>());
    }
}