using BarberBilling.Application.UseCases.User;
using BarberBilling.Communication.Requests.Users;
using FluentValidation;
using Shouldly;

namespace BarberBilling.Tests.Validator.Tests.User;

public class PasswordValidatorTests
{
    [Theory]
    [MemberData(nameof(RequestPasswordInvalid.InvalidPassword), MemberType = typeof(RequestPasswordInvalid))]
    public void Error_Password_Invalid(string? password)
    {    
        var validator = new PasswordValidator<RequestRegisterUserJson>();
        var context = new ValidationContext<RequestRegisterUserJson>(new RequestRegisterUserJson());
        var result = validator
            .IsValid(context, password!);

        result.ShouldBeFalse();
    }
}