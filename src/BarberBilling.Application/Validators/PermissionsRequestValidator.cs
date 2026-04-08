using BarberBilling.Communication.Requests.Authorization;
using BarberBilling.Exceptions.CustomExceptions;
using FluentValidation;

namespace BarberBilling.Application.Validators;

public class PermissionsRequestValidator : AbstractValidator<RequestPermissionsJson>
{
    public PermissionsRequestValidator()
    {
        RuleFor(r => r.PermissionIds)
            .NotNull().WithMessage("permissionsRequired")
            .NotEmpty().WithMessage("permissionsRequired")
            .Must(ids => ids is not null && ids.All(id => id != Guid.Empty))
            .WithMessage("permissionIdentifierRequired");
    }

    public void ValidateInput(RequestPermissionsJson request)
    {
        var result = Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
