
namespace Validators.Tests.Users.Register;

public class RegisterUserValidatorTests
{
    // [Fact]
    // public void Success()
    // {
    //     // Arrange
    //     var validator = new UserValidator();
    //     var request = RequestRegisterUserJsonBuilder.Build();

    //     // Act
    //     var result = validator.Validate(request);

    //     // Assert
    //     result.IsValid.ShouldBeTrue();
    // }

    // [Theory]
    // [InlineData("")]
    // [InlineData(" ")]
    // public void Failure_When_Name_Is_Null_Or_Empty(string name)
    // {
    //     // Arrange
    //     var validator = new UserValidator();
    //     var request = RequestRegisterUserJsonBuilder.Build();
    //     request.Name = name;

    //     // Act
    //     var result = validator.Validate(request);

    //     // Assert
    //     result.IsValid.ShouldBeFalse();
    //     result.Errors.ShouldSatisfyAllConditions(
    //         errors => errors.ShouldContain(e => e.ErrorMessage == "nameRequired"),
    //         errors => errors.Count.ShouldBe(1)
    //     );
    // }

    // [Fact]
    // public void Failure_When_Name_Is_Too_Short()
    // {
    //     // Arrange
    //     var validator = new UserValidator();
    //     var request = RequestRegisterUserJsonBuilder.Build();
    //     request.Name = "Yo";

    //     // Act
    //     var result = validator.Validate(request);

    //     // Assert
    //     result.IsValid.ShouldBeFalse();
    //     result.Errors.ShouldContain(e => e.ErrorMessage == "nameTooShort");
    // }

    // [Fact]
    // public void Failure_When_Email_Is_Invalid()
    // {
    //     // Arrange
    //     var validator = new UserValidator();
    //     var request = RequestRegisterUserJsonBuilder.Build();
    //     request.Email = "invalid-email";

    //     // Act
    //     var result = validator.Validate(request);

    //     // Assert
    //     result.IsValid.ShouldBeFalse();
    //     result.Errors.ShouldContain(e => e.ErrorMessage == "emailInvalid");
    // }

    
    // [Theory]
    // [InlineData("")]
    // [InlineData("emailmuitorandomicoquepassaolimitemaximode150caractereseeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee@test.com")]
    // [InlineData("emailinvalido")]
    // public void Failure_When_Email_Is_Empty(string email)
    // {
    //     // Arrange
    //     var validator = new UserValidator();
    //     var request = RequestRegisterUserJsonBuilder.Build();
    //     request.Email = email;

    //     // Act
    //     var result = validator.Validate(request);

    //     // Assert
    //     result.IsValid.ShouldBeFalse();
    //     result.Errors.ShouldContain(e => e.ErrorMessage == "emailRequired");
    // }

    // [Fact]
    // public void Failure_When_Password_Is_Too_Short()
    // {
    //     // Arrange
    //     var validator = new UserValidator();
    //     var request = RequestRegisterUserJsonBuilder.Build();
    //     request.Password = "Ab1";

    //     // Act
    //     var result = validator.Validate(request);

    //     // Assert
    //     result.IsValid.ShouldBeFalse();
    //     result.Errors.ShouldContain(e => e.ErrorMessage == "passwordTooShort");
    // }

    // [Fact]
    // public void Failure_When_Password_Has_No_Uppercase()
    // {
    //     // Arrange
    //     var validator = new UserValidator();
    //     var request = RequestRegisterUserJsonBuilder.Build();
    //     request.Password = "password1";

    //     // Act
    //     var result = validator.Validate(request);

    //     // Assert
    //     result.IsValid.ShouldBeFalse();
    //     result.Errors.ShouldContain(e => e.ErrorMessage == "passwordMustContainUppercase");
    // }

    // [Fact]
    // public void Failure_When_Password_Has_No_Lowercase()
    // {
    //     // Arrange
    //     var validator = new UserValidator();
    //     var request = RequestRegisterUserJsonBuilder.Build();
    //     request.Password = "PASSWORD1";

    //     // Act
    //     var result = validator.Validate(request);

    //     // Assert
    //     result.IsValid.ShouldBeFalse();
    //     result.Errors.ShouldContain(e => e.ErrorMessage == "passwordMustContainLowercase");
    // }

    // [Fact]
    // public void Failure_When_Password_Has_No_Number()
    // {
    //     // Arrange
    //     var validator = new UserValidator();
    //     var request = RequestRegisterUserJsonBuilder.Build();
    //     request.Password = "Password";

    //     // Act
    //     var result = validator.Validate(request);

    //     // Assert
    //     result.IsValid.ShouldBeFalse();
    //     result.Errors.ShouldContain(e => e.ErrorMessage == "passwordMustContainNumber");
    // }
}