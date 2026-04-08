using BarberBilling.Application.Validators;
using BarberBilling.Communication.Enums;
using BarberBilling.Communication.Requests.Bookings;
using Shouldly;

namespace Validators.Tests.Bookings;

public class BookingUpdateValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new BookingUpdateValidator();
        var request = new BookingUpdateRequestJson(BookingStatus.Confirmed, "Notes updated");

        var result = validator.Validate(request);
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Error_Status_Invalid()
    {
        var validator = new BookingUpdateValidator();
        var request = new BookingUpdateRequestJson((BookingStatus)999, "Notes updated");

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("statusInvalid"));
    }

    [Fact]
    public void Error_Notes_TooLong()
    {
        var validator = new BookingUpdateValidator();
        var request = new BookingUpdateRequestJson(BookingStatus.Pending, new string('a', 501));

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("notesTooLong"));
    }
}
