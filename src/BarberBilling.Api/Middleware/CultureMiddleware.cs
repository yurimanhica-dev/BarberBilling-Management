using System.Globalization;

namespace BarberBilling.Api.Middleware;

public class CultureMiddleware
{
    private readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var supportedCultures = new[] { "en", "pt" };

        var cultureName = context.Request.Headers.AcceptLanguage
            .FirstOrDefault()
            ?.Split(',')           // "pt-BR,en;q=0.9" → ["pt-BR", "en;q=0.9"]
            .FirstOrDefault()
            ?.Split(';')           // "pt-BR" or "pt-BR;q=1"
            .FirstOrDefault()
            ?.Trim()
            ?? "en";

        // Normalize: "pt-BR" → "pt", "en-US" → "en"
        var twoLetter = cultureName.Split('-')[0];

        // Fallback to "en" if not supported
        var finalCulture = supportedCultures.Contains(twoLetter) ? twoLetter : "en";

        var cultureInfo = new CultureInfo(finalCulture);

        CultureInfo.CurrentCulture = cultureInfo;
        CultureInfo.CurrentUICulture = cultureInfo;

        await _next(context);
    }
}