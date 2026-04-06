using System.Security.Claims;
using System.Text;
using BarberBilling.Domain.Repositories.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BarberBilling.Api.Configuration;

public static class AuthenticationExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(config =>
        {
            config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(config =>
        {
            config.TokenValidationParameters = GetTokenValidationParameters(configuration);
            config.Events = GetJwtEvents();
        });
    }

    private static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(configuration["Settings:Jwt:Key"]!)
        );

        var validIssuer = configuration["Settings:Jwt:ValidIssuer"] ?? "BarberBilling";
        var validAudience = configuration["Settings:Jwt:ValidAudience"] ?? "BarberBillingApp";

        return new TokenValidationParameters
        {
            IssuerSigningKey = key,
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidIssuer = validIssuer,
            ValidateAudience = true,
            ValidAudience = validAudience,
            ClockSkew = TimeSpan.Zero
        };
    }

    private static JwtBearerEvents GetJwtEvents()
    {
        return new JwtBearerEvents
        {
            OnTokenValidated = async context =>
            {
                var userId = context.Principal?.FindFirst(ClaimTypes.Sid)?.Value;
                var tokenVersion = context.Principal?.FindFirst("token_version")?.Value;

                if (userId is null || tokenVersion is null)
                {
                    context.Fail("Token inválido.");
                    return;
                }

                var repository = context.HttpContext.RequestServices
                    .GetRequiredService<IUserReadOnlyRepository>();

                var user = await repository.GetByIdentifier(Guid.Parse(userId));

                if (user is null || user.TokenVersion.ToString() != tokenVersion)
                {
                    context.Fail("Token expirado.");
                }
            }
        };
    }
}