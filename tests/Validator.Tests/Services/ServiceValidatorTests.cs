using BarberBilling.Application.Validators;
using BarberBilling.Communication.Requests.Services;
using Shouldly;

namespace Validators.Tests.Services;

public class ServiceValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new ServiceValidator();
        var request = new RequestServiceJson
        {
            Services = BarberBilling.Communication.Enums.Services.HaircutAndBeard,
            Price = 100m,
            Category = BarberBilling.Communication.Enums.Category.Haircut
        };

        var result = validator.Validate(request);
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Error_Service_Invalid()
    {
        var validator = new ServiceValidator();
        var request = new RequestServiceJson
        {
            Services = (BarberBilling.Communication.Enums.Services)999,
            Price = 100m,
            Category = BarberBilling.Communication.Enums.Category.Haircut
        };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("serviceInvalid"));
    }

    [Fact]
    public void Error_Price_MustBeGreaterThanZero()
    {
        var validator = new ServiceValidator();
        var request = new RequestServiceJson
        {
            Services = BarberBilling.Communication.Enums.Services.HaircutAndBeard,
            Price = 0m,
            Category = BarberBilling.Communication.Enums.Category.Haircut
        };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("priceMustBeGreaterThanZero"));
    }

    [Fact]
    public void Error_Category_Invalid()
    {
        var validator = new ServiceValidator();
        var request = new RequestServiceJson
        {
            Services = BarberBilling.Communication.Enums.Services.HaircutAndBeard,
            Price = 100m,
            Category = (BarberBilling.Communication.Enums.Category)999
        };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("categoryInvalid"));
    }
}
