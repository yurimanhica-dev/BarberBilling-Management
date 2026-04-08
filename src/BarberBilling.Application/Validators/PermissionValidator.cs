using BarberBilling.Communication.Requests.Authorization;
using BarberBilling.Exceptions.CustomExceptions;
using FluentValidation;

namespace BarberBilling.Application.Validators;

public class PermissionValidator : AbstractValidator<RequestRegisterPermissionJson>
{
    public PermissionValidator()
    {
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("permissionRequired")
            .MinimumLength(3).WithMessage("permissionNameTooShort")
            .MaximumLength(100).WithMessage("permissionNameTooLong");
    }

    public void ValidateInput(RequestRegisterPermissionJson request)
    {
        var result = Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
