namespace BarberBilling.Tests.CommonTestUtilities.helpers;

public static class RequestInvalidEmail
{
    public static IEnumerable<object[]> InvalidEmails =>
    [
        [""],
        [" "],
        ["plaintext"],           // sem @
        ["missing@"],            // sem domínio
        ["@missing_local.com"],   // sem local
        ["missing.domain@"],     // sem extensão
        ["two@@domain.com"],     // dois @
    ];
}