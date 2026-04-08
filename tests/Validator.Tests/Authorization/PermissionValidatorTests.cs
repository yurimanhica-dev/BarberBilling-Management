using BarberBilling.Application.Validators;
using BarberBilling.Communication.Requests.Authorization;
using Shouldly;

namespace Validators.Tests.Authorization;

public class PermissionValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new PermissionValidator();
        var request = new RequestRegisterPermissionJson { Name = "CreateUser" };

        var result = validator.Validate(request);
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Error_Name_IsEmpty()
    {
        var validator = new PermissionValidator();
        var request = new RequestRegisterPermissionJson { Name = string.Empty };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("permissionRequired"));
    }

    [Fact]
    public void Error_Name_TooShort()
    {
        var validator = new PermissionValidator();
        var request = new RequestRegisterPermissionJson { Name = "ab" };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("permissionNameTooShort"));
    }

    [Fact]
    public void Error_Name_TooLong()
    {
        var validator = new PermissionValidator();
        var request = new RequestRegisterPermissionJson { Name = new string('a', 101) };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("permissionNameTooLong"));
    }
}
