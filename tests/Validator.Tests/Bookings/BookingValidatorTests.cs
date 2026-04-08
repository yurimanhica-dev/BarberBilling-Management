using BarberBilling.Application.Validators;
using BarberBilling.Communication.Requests.Bookings;
using BarberBilling.Tests.CommonTestUtilities.helpers;
using BarberBilling.Tests.CommonTestUtilities.Requests;
using Shouldly;

namespace Validators.Tests.Bookings;

public class BookingValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new BookingValidator();
        var request = BookingRequestJsonBuilder.Build();
        var result = validator.Validate(request);
        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [MemberData(nameof(RequestEmptyGuid.EmptyGuids), MemberType = typeof(RequestEmptyGuid))]
    public void Error_BarberIdentifier_IsEmpty(Guid barberId)
    {
        var validator = new BookingValidator();
        var request = BookingRequestJsonBuilder.Build();
        request = request with { BarberIdentifier = barberId };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("barberRequired"));
    }

    [Fact]
    public void Error_ServiceIds_IsEmpty()
    {
        var validator = new BookingValidator();
        var request = BookingRequestJsonBuilder.Build();
        request = request with { ServiceIds = new List<Guid>() };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e =>
            e.ErrorMessage.Equals("servicesRequired") ||
            e.ErrorMessage.Equals("atLeastOneServiceRequired"));
    }

    [Fact]
    public void Error_ServiceIds_Null()
    {
        var validator = new BookingValidator();
        var request = BookingRequestJsonBuilder.Build();
        request = request with { ServiceIds = null! };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("servicesRequired"));
    }

    [Theory]
    [MemberData(nameof(RequestDate.PastDates), MemberType = typeof(RequestDate))]
    public void Error_ScheduledDate_IsPast(DateTime pastDate)
    {
        var validator = new BookingValidator();
        var request = BookingRequestJsonBuilder.Build();
        request = request with { ScheduledDate = pastDate };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("ScheduledDateCannotBePast"));
    }

    [Fact]
    public void Success_WithNotes()
    {
        var validator = new BookingValidator();
        var request = BookingRequestJsonBuilder.Build();
        request = request with { Notes = "Additional notes about this booking" };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Success_WithoutNotes()
    {
        var validator = new BookingValidator();
        var request = BookingRequestJsonBuilder.Build();
        request = request with { Notes = null };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }
}
