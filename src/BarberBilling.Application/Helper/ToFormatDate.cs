using System.Globalization;

namespace BarberBilling.Application.Helper;

public static class ToFormatDate
{
    public static string ToDate(this DateTime date, CultureInfo? culture = null)
    {
        culture ??= CultureInfo.CurrentCulture;

        var formatted = date.ToString("D", culture);

        // Só aplica regra especial para português
        if (culture.TwoLetterISOLanguageName == "pt")
        {
            var textInfo = culture.TextInfo;

            formatted = textInfo.ToTitleCase(formatted)
                .Replace(" De ", " de ")
                .Replace(" Da ", " da ")
                .Replace(" Do ", " do ");
        }

        return formatted;
    }
}