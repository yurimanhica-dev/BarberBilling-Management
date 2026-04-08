using BarberBilling.Tests.CommonTestUtilities.helpers;
using BarberBilling.Tests.CommonTestUtilities.Requests;
using Shouldly;

namespace Validators.Tests.Users.Register;

public class RegisterUserValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new UserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        var result = validator.Validate(request);
        result.IsValid.ShouldBeTrue();
    }
    
    [Theory]
    [MemberData(nameof(RequestIsNullOrWhiteSpace.InvalidValues), MemberType = typeof(RequestIsNullOrWhiteSpace))]
    public void Error_Name_IsEmpty(string? name)
    {
        var validator = new UserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = name!;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("nameRequired"));
    }

    [Theory]
    [MemberData(nameof(RequestLongAndShortName.ShortNames), MemberType = typeof(RequestLongAndShortName))]
    public void Error_Name_TooShort(string name)
    {
        var validator = new UserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = name;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("nameTooShort"));
    }

    [Theory]
    [MemberData(nameof(RequestLongAndShortName.LongNames), MemberType = typeof(RequestLongAndShortName))]
    public void Error_Name_TooLong(string name)
    {
        var validator = new UserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Name = name;

        var result = validator.Validate(request);
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("nameTooLong"));
    }

    [Theory]
    [MemberData(nameof(RequestIsNullOrWhiteSpace.InvalidValues), MemberType = typeof(RequestIsNullOrWhiteSpace))]
    public void Error_Email_IsEmpty(string? email)
    {
        var validator = new UserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = email!;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("emailRequired"));
    }

    [Theory]
    [MemberData(nameof(RequestInvalidEmail.InvalidEmails), MemberType = typeof(RequestInvalidEmail))]
    public void Error_Email_Invalid(string email)
    {
        var validator = new UserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = email;

        var result = validator.Validate(request);
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("emailInvalid"));
    }

    [Theory]
    [MemberData(nameof(RequestLongAndShortName.LongNames), MemberType = typeof(RequestLongAndShortName))]
    public void Error_Email_TooLong(string email)
    {
        var validator = new UserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Email = email;

        var result = validator.Validate(request);
        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("emailTooLong"));
    }

    [Theory]
    [MemberData(nameof(RequestIsNullOrWhiteSpace.InvalidValues), MemberType = typeof(RequestIsNullOrWhiteSpace))]
    public void Error_Password_IsEmpty(string? password)
    {
        var validator = new UserValidator();
        var request = RequestRegisterUserJsonBuilder.Build();
        request.Password = password!;

        var result = validator.Validate(request);

        result.IsValid.ShouldBeFalse();
        result.Errors.ShouldContain(e => e.ErrorMessage.Equals("passwordIsEmpty"));
    }
}