using BarberBilling.Communication.Requests.Authorization;
using BarberBilling.Exceptions.CustomExceptions;
using FluentValidation;

namespace BarberBilling.Application.Validators;

public class RoleValidator : AbstractValidator<RequestCreateRoleJson>
{
    public RoleValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("roleRequired")
            .MinimumLength(3).WithMessage("roleNameTooShort")
            .MaximumLength(100).WithMessage("roleNameTooLong");
    }

    public void ValidateInput(RequestCreateRoleJson request)
    {
        var result = Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
