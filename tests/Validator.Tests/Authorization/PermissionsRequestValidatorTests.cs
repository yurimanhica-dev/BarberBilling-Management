using BarberBilling.Application.Validators;
using BarberBilling.Communication.Requests.Authorization;
using Shouldly;

namespace Validators.Tests.Authorization;

public class PermissionsRequestValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new PermissionsRequestValidator();
        var request = new RequestPermissionsJson { PermissionIds = new List<Guid> { Guid.NewGuid() } };

        var result = validator.Validate(request);
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Error_Permissions_IsEmpty()
    {
        var validator = new PermissionsRequestValidator();
        var request = new RequestPermissionsJson { PermissionIds = new List<Guid>() };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("permissionsRequired"));
    }

    [Fact]
    public void Error_Permissions_Null()
    {
        var validator = new PermissionsRequestValidator();
        var request = new RequestPermissionsJson { PermissionIds = null! };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("permissionsRequired"));
    }

    [Fact]
    public void Error_PermissionIdentifier_IsEmpty()
    {
        var validator = new PermissionsRequestValidator();
        var request = new RequestPermissionsJson { PermissionIds = new List<Guid> { Guid.Empty } };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("permissionIdentifierRequired"));
    }
}
