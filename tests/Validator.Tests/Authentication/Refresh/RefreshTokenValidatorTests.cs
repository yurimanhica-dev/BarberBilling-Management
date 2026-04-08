using BarberBilling.Application.Validators;
using BarberBilling.Communication.Requests.Authentication.RefreshToken;
using Shouldly;

namespace Validators.Tests.Authentication.Refresh;

public class RefreshTokenValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new RefreshTokenValidator();
        var request = new RequestRefreshTokenJson { RefreshToken = "refresh-token-123" };

        var result = validator.Validate(request);
        result.IsValid.ShouldBeTrue();
    }

    [Fact]
    public void Error_RefreshToken_IsEmpty()
    {
        var validator = new RefreshTokenValidator();
        var request = new RequestRefreshTokenJson { RefreshToken = string.Empty };

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("refreshTokenRequired"));
    }
}
