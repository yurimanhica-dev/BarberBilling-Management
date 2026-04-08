using BarberBilling.Application.Validators;
using BarberBilling.Communication.Requests.Authentication.login;
using Shouldly;

namespace Validators.Tests.Authentication.Login;

public class LoginValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new LoginValidator();
        var request = new RequestLoginJson("test@example.com", "Password1!");

        var result = validator.Validate(request);
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Error_Email_IsEmpty()
    {
        var validator = new LoginValidator();
        var request = new RequestLoginJson(string.Empty, "Password1!");

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("emailRequired"));
    }

    [Fact]
    public void Error_Email_Invalid()
    {
        var validator = new LoginValidator();
        var request = new RequestLoginJson("invalid-email", "Password1!");

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("emailInvalid"));
    }

    [Fact]
    public void Error_Password_IsEmpty()
    {
        var validator = new LoginValidator();
        var request = new RequestLoginJson("test@example.com", string.Empty);

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("passwordIsEmpty"));
    }
}
