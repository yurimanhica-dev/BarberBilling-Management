namespace BarberBilling.Tests.Validator.Tests.User;

public static class RequestPasswordInvalid
{
    public static IEnumerable<object[]> InvalidPassword =>
    [
        [""],                          // vazio
        [" "],                         // só espaço — hasWhiteSpace
        ["Aaaaa1!"],                  // 7 chars válidos mas curto demais — hasMiniMaxChars
        ["aaaaaaa"],                   // 7 chars, sem maiúscula, número, especial
        ["BBBBBBB"],                   // 7 chars, sem minúscula, número, especial
        ["12345678"],                  // 8 chars, sem letra, especial
        ["aaaaaaaa"],                  // 8 chars, sem maiúscula, número, especial
        ["AAAAAAAA"],                  // 8 chars, sem minúscula, número, especial
        ["Aaaaaaaa"],                  // 8 chars, sem número, especial
        ["Aaaaaaa1"],                  // 8 chars, sem especial
        ["Aa1! Aaa"],                  // tem espaço — hasWhiteSpace
    ];
}