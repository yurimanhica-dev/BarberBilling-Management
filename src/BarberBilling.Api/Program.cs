using BarberBilling.Api.Configuration;
using BarberBilling.Api.Middleware;
using BarberBilling.Application;
using BarberBilling.Application.Settings;
using BarberBilling.Infrastructure;
using BarberBilling.Infrastructure.Migrations;
using ExpenseManagement.Api.Filters;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddMvc(options => { options.Filters.Add<ExceptionFilter>(); });
builder.Services.AddLocalization();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddPermissionAuthorization();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowDevelopment", policy =>
    {
        var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()
            ?? new[] { "http://localhost:3000", "http://localhost:5173" };

        policy.WithOrigins(allowedOrigins)
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.Configure<CompanySettings>(
builder.Configuration.GetSection("CompanySettings"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(o =>
        {
            o.Title = "Barber Billing Management API";
            o.WithTheme(ScalarTheme.BluePlanet);
            o.ForceDarkMode();
        });
    app.UseDeveloperExceptionPage();
}

// var localizationOptions = LocalizationConfig.GetRequestLocalizationOptions();
// app.UseRequestLocalization(localizationOptions);

app.UseMiddleware<CultureMiddleware>();

app.UseCors("AllowDevelopment");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await MigrateDatabase();

app.Run();

async Task MigrateDatabase()
{
    await using var scope = app.Services.CreateAsyncScope();
    await DataBaseMigration.MigrateDataBase(scope.ServiceProvider);
}