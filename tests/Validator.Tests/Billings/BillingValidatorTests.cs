using BarberBilling.Application.Validators;
using BarberBilling.Communication.Enums;
using BarberBilling.Tests.CommonTestUtilities.helpers;
using BarberBilling.Tests.CommonTestUtilities.Requests;
using Shouldly;

namespace Validators.Tests.Billings;

public class BillingValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new BillingValidator( );
        var request = BillingRequestJsonBuilder.Build();
        var result = validator.Validate(request);
        result.IsValid.ShouldBeTrue();
    }

    [Theory]
    [MemberData(nameof(RequestEmptyGuid.EmptyGuids), MemberType = typeof(RequestEmptyGuid))]
    public void Error_ClientIdentifier_IsEmpty(Guid clientId)
    {
        var validator = new BillingValidator();
        var request = BillingRequestJsonBuilder.Build();
        request = request with { ClientIdentifier = clientId };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("clientRequired"));
    }

    [Fact]
    public void Error_ServiceIds_IsEmpty()
    {
        var validator = new BillingValidator();
        var request = BillingRequestJsonBuilder.Build();
        request = request with { ServiceIds = new List<Guid>() };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("servicesRequired"));
    }

    [Fact]
    public void Error_ServiceIds_Null()
    {
        var validator = new BillingValidator();
        var request = BillingRequestJsonBuilder.Build();
        request = request with { ServiceIds = null! };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("servicesRequired"));
    }

    [Theory]
    [MemberData(nameof(RequestDate.FutureDates), MemberType = typeof(RequestDate))]
    public void Error_Date_IsFuture(DateTime futureDate)
    {
        var validator = new BillingValidator();
        var request = BillingRequestJsonBuilder.Build();
        request = request with { Date = futureDate };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("DateCannotBeFuture"));
    }

    [Fact]
    public void Error_PaymentMethod_Invalid()
    {
        var validator = new BillingValidator();
        var request = BillingRequestJsonBuilder.Build();
        // Criar um valor inválido de enum
        request = request with { PaymentMethod = (PaymentMethod)999 };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("PaymentMethodInvalid"));
    }

    [Fact]
    public void Error_Status_Invalid()
    {
        var validator = new BillingValidator();
        var request = BillingRequestJsonBuilder.Build();
        // Criar um valor inválido de enum
        request = request with { Status = (Status)999 };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("StatusInvalid"));
    }

    [Fact]
    public void Success_WithNotes()
    {
        var validator = new BillingValidator();
        var request = BillingRequestJsonBuilder.Build();
        request = request with { Notes = "Additional notes about this billing" };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Success_WithoutNotes()
    {
        var validator = new BillingValidator();
        var request = BillingRequestJsonBuilder.Build();
        request = request with { Notes = null };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeTrue();
    }
}
