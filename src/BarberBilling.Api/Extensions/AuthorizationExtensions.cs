using BarberBilling.Api.Security.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace BarberBilling.Api.Configuration;

public static class AuthorizationExtensions
{
    public static void AddPermissionAuthorization(this IServiceCollection services)
    {
        // Regista o handler
        services.AddScoped<IAuthorizationHandler, PermissionHandler>();

        services.AddAuthorization(options =>
        {
            // Regista automaticamente todas as policies
            var permissions = typeof(Permissions)
                .GetNestedTypes()
                .SelectMany(t => t.GetFields())
                .Select(f => f.GetValue(null)?.ToString())
                .Where(p => p is not null)
                .Cast<string>();

            foreach (var permission in permissions)
            {
                options.AddPolicy(permission, policy =>
                    policy.Requirements.Add(new PermissionRequirement(permission)));
            }
        });
    }
}