using System.Security.Claims;
using System.Text;
using BarberBilling.Domain.Repositories.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace BarberBilling.Api.Extensions;

public static class AuthenticationExtension
{
    public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Settings:Jwt:Key"]!));

        services.AddAuthentication(config =>
        {
            config.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(config =>
        {
            // Valida assinatura do JWT
            config.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = signingKey,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            // Valida TokenVersion após assinatura — invalida access tokens antigos
            config.Events = new JwtBearerEvents
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
        });

        services.AddAuthorization();
    }
}