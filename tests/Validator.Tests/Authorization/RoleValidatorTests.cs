using BarberBilling.Application.Validators;
using BarberBilling.Communication.Requests.Authorization;
using Shouldly;

namespace Validators.Tests.Authorization;

public class RoleValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new RoleValidator();
        var request = new RequestCreateRoleJson { Name = "Administrator" };

        var result = validator.Validate(request);
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Error_Name_IsEmpty()
    {
        var validator = new RoleValidator();
        var request = new RequestCreateRoleJson { Name = string.Empty };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("roleRequired"));
    }

    [Fact]
    public void Error_Name_TooShort()
    {
        var validator = new RoleValidator();
        var request = new RequestCreateRoleJson { Name = "ab" };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("roleNameTooShort"));
    }

    [Fact]
    public void Error_Name_TooLong()
    {
        var validator = new RoleValidator();
        var request = new RequestCreateRoleJson { Name = new string('a', 101) };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("roleNameTooLong"));
    }
}
