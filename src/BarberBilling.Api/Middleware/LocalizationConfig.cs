using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;

namespace BarberBilling.Api.Localization
{
    public static class LocalizationConfig
    {
        public static RequestLocalizationOptions GetRequestLocalizationOptions()
        {
            var supportedCultures = new[]
            {
                new CultureInfo("en"),    // default
                new CultureInfo("pt")     // português
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            return options;
        }
    }
}