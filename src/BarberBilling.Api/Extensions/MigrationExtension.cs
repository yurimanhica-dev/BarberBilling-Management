using BarberBilling.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BarberBilling.API.Extensions;

public static class MigrationExtension
{
    public static async Task MigrateDatabase(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();
    }
}