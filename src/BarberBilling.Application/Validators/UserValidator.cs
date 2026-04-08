using BarberBilling.Application.UseCases.User;
using BarberBilling.Communication.Requests.Users;
using FluentValidation;

public class UserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public UserValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("nameRequired")
            .MinimumLength(3).WithMessage("nameTooShort")
            .MaximumLength(100).WithMessage("nameTooLong");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("emailRequired")
            .EmailAddress().WithMessage("emailInvalid")
            .MaximumLength(100).WithMessage("emailTooLong");

        RuleFor(u => u.RoleIdentifier)
            .NotEmpty().WithMessage("roleRequired");

        RuleFor(u => u.Password)
            .SetValidator(new PasswordValidator<RequestRegisterUserJson>());
    }
}