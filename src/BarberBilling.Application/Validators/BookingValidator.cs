using BarberBilling.Communication.Requests.Bookings;
using BarberBilling.Exceptions.CustomExceptions;
using FluentValidation;

namespace BarberBilling.Application.Validators;

public class BookingValidator : AbstractValidator<BookingRequestJson>
{
    public BookingValidator()
    {
        RuleFor(b => b.ScheduledDate)
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("ScheduledDateCannotBePast");

        RuleFor(b => b.BarberIdentifier)
            .NotEmpty().WithMessage("barberRequired");

        RuleFor(b => b.ServiceIds)
            .NotEmpty().WithMessage("servicesRequired")
            .Must(s => s.Count > 0)
            .When(b => b.ServiceIds is not null, ApplyConditionTo.CurrentValidator)
            .WithMessage("atLeastOneServiceRequired");
    }

    public void ValidateInput(BookingRequestJson request)
    {
        var result = Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
