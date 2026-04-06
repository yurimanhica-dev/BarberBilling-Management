using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Billings;
using BarberBilling.Domain.Repositories.Categories;
using BarberBilling.Domain.Repositories.Services;
using BarberBilling.Domain.Repositories.Token;
using BarberBilling.Domain.Repositories.User;
using BarberBilling.Domain.Repositories.User.Authorization;
using BarberBilling.Domain.Security.Cryptography;
using BarberBilling.Domain.Security.Tokens;
using BarberBilling.Infrastructure.Context;
using BarberBilling.Infrastructure.Persistence;
using BarberBilling.Infrastructure.Persistence.Repositories;
using BarberBilling.Infrastructure.Security.Cryptography;
using BarberBilling.Infrastructure.Security.Tokens;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBilling.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddApplicationDbContext(services, configuration);
        AddToken(services, configuration);
        AddSettings(services, configuration);
        AddRepositories(services);
    }
    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var ExpirationInMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationInMinutes");
        var secretKey = configuration.GetValue<string>("Settings:Jwt:Key");
        var refreshTokenExpirationInDays = configuration.GetValue<uint>("Settings:Jwt:RefreshTokenExpirationInDays");
        services.AddScoped<IAccessTokenGenerator>(settings => new JwtTokenGenerator(ExpirationInMinutes, secretKey!, refreshTokenExpirationInDays));

        services.AddScoped<IRefreshTokenGenerator>(settings => new JwtTokenGenerator(ExpirationInMinutes, secretKey!, refreshTokenExpirationInDays));
    }
    private static void AddSettings(IServiceCollection services, IConfiguration configuration)
    {
        var Security = configuration.GetSection("Security:Pepper");
        services.AddScoped<IPasswordEncripte>(settings => new Argon2id(Security.Value!));
    }
    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IBillingWriteOnlyRepository, BillingRepository>();
        services.AddScoped<IBillingReadOnlyRepository, BillingRepository>();
        services.AddScoped<IBillingUpdateOnlyRepository, BillingRepository>();

        services.AddScoped<IServiceReadOnlyRepository, ServiceRepository>();
        services.AddScoped<IServiceUpdateOnlyRepository, ServiceRepository>();
        services.AddScoped<IServiceWriteOnlyRepository, ServiceRepository>();

        services.AddScoped<ICategoryReadOnlyRepository, CategoryRepository>();

        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();

        services.AddScoped<IAuthorizationReadOnlyRepository, AuthorizationRepository>();
        services.AddScoped<IAuthorizationWriteOnlyRepository, AuthorizationRepository>();

        services.AddScoped<ITokenReadOnlyRepository, TokenRepository>();
        services.AddScoped<ITokenWriteOnlyRepository, TokenRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }
    private static void AddApplicationDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? configuration["DATABASE_URL"];

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(
            connectionString,
            o => o.EnableRetryOnFailure()
        ));
    }
}