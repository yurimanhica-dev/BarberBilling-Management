using System.Text.RegularExpressions;
using FluentValidation;
using FluentValidation.Validators;

namespace BarberBilling.Application.UseCases.User;

public partial class PasswordValidator<T> : PropertyValidator<T, string>
{
    private const string ErrorMessageKey = "ErrorMessage";
    public PasswordValidator()
    {
    }

    public override string Name => "PasswordValidator";
    protected override string GetDefaultMessageTemplate(string errorCode) => $"{{{ErrorMessageKey}}}";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            var msg = "passwordIsEmpty"; // _localizer["passwordIsEmpty"];
            context.MessageFormatter.AppendArgument(ErrorMessageKey, msg);
            return false;
        }

        var hasWhiteSpace = HasWhiteSpace().IsMatch(password);
        var hasSpecialChar = HasSpecialChar().IsMatch(password);
        var hasNumber = HasNumber().IsMatch(password);
        var hasUpperChar = HasUpperChar().IsMatch(password);
        var hasMiniMaxChars = HasValidLength().IsMatch(password);
        var hasLowerChar = HasLowerChar().IsMatch(password);

        if (!hasSpecialChar)
        {
            var msg = "passwordNotStrongEnough"; // _localizer["InvalidSpecialCharacter"];
            context.MessageFormatter.AppendArgument(ErrorMessageKey, msg);
            return false;
        }

        if (!hasNumber || !hasUpperChar || !hasMiniMaxChars || !hasLowerChar || hasWhiteSpace)
        {
            var msg = "passwordNotStrongEnough"; // _localizer["passwordNotStrongEnough"];
            context.MessageFormatter.AppendArgument(ErrorMessageKey, msg);
            return false;
        }

        return true;
    }

    [GeneratedRegex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]")]
    private static partial Regex HasSpecialChar();
    [GeneratedRegex(@"[0-9]+")]
    private static partial Regex HasNumber();
    [GeneratedRegex(@"\s")]
    private static partial Regex HasWhiteSpace();
    [GeneratedRegex(@"[A-Z]+")]
    private static partial Regex HasUpperChar();
    [GeneratedRegex(@".{8,15}")]
    private static partial Regex HasValidLength();
    [GeneratedRegex(@"[a-z]+")]
    private static partial Regex HasLowerChar();
}