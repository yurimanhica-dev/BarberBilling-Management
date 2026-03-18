using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Billings;
using BarberBilling.Infrastructure.Context;
using BarberBilling.Infrastructure.Persistence;
using BarberBilling.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBilling.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddApplicationDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IBillingWriteOnlyRepository, BillingRepository>();
        services.AddScoped<IBillingReadOnlyRepository, BillingRepository>();
        services.AddScoped<IBillingUpdateOnlyRepository, BillingRepository>();
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