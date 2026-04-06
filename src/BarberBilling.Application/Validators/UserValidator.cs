using BarberBilling.Application.Extensions;
using BarberBilling.Application.UseCases.User;
using BarberBilling.Communication.Requests.Authentication.RegisterClient;
using BarberBilling.Communication.Requests.Users;
using ExpenseManagement.Exception;
using FluentValidation;
using Microsoft.Extensions.Localization;

public class UserValidator : AbstractValidator<RequestRegisterUserJson>
{
    public UserValidator(IStringLocalizer<ErrorMessages> localizer)
    {
        SetupRules(localizer);
    }

    public UserValidator() : this(new NullStringLocalizer<ErrorMessages>())
    {
    }

    private void SetupRules(IStringLocalizer<ErrorMessages> localizer)
    {
        RuleFor(u => u.Name)
            .NotEmpty().WithMessage("nameRequired")
            .MinimumLength(3).WithMessage("nameTooShort")
            .MaximumLength(100).WithMessage("nameTooLong");

        RuleFor(u => u.Email)
            .NotEmpty().WithMessage("emailRequired")
            .EmailAddress().WithMessage("emailInvalid")
            .MaximumLength(150).WithMessage("emailTooLong");

        RuleFor(u => u.RoleIdentifier)
            .NotEmpty().WithMessage("roleRequired");

        RuleFor(u => u.Password)
            .SetValidator(new PasswordValidator<RequestRegisterUserJson>(localizer));
    }
}