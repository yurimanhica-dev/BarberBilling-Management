using System.Globalization;

namespace BarberBilling.Application.Helper;

public static class CurrencyFormatter
{
    public static string FormatCurrency(decimal value)
    {
        var culture = CultureInfo.CurrentCulture;
        // string formatted = value < 1000
        //     ? value.ToString("0.00", culture)   // remove zeros à esquerda
        //     : value.ToString("#,0.00", culture); // separador de milhar

        if (Math.Abs(value) < 1000)
            return value.ToString("0.00", culture);

        return value.ToString("#,0.00", culture);    
    }
}