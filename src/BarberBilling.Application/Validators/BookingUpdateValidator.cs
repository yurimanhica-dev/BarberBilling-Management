using BarberBilling.Communication.Requests.Bookings;
using BarberBilling.Exceptions.CustomExceptions;
using FluentValidation;

namespace BarberBilling.Application.Validators;

public class BookingUpdateValidator : AbstractValidator<BookingUpdateRequestJson>
{
    public BookingUpdateValidator()
    {
        RuleFor(b => b.Status)
            .IsInEnum().WithMessage("statusInvalid");

        RuleFor(b => b.Notes)
            .MaximumLength(500).WithMessage("notesTooLong")
            .When(b => b.Notes is not null);
    }

    public void ValidateInput(BookingUpdateRequestJson request)
    {
        var result = Validate(request);
        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
