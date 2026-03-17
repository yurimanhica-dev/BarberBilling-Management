using BarberBilling.Api.Localization;
using BarberBilling.API.Extensions;
using BarberBilling.Application;
using BarberBilling.Application.Settings;
using BarberBilling.Infrastructure;
using ExpenseManagement.Api.Filters;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddMvc(options => { options.Filters.Add<ExceptionFilter>(); });
builder.Services.AddLocalization();

builder.Services.Configure<CompanySettings>(
builder.Configuration.GetSection("CompanySettings"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(o =>
        {
            o.Title = "Barber Billing Management API";
            o.WithTheme(ScalarTheme.BluePlanet);
            o.ForceDarkMode();
        });
}

var localizationOptions = LocalizationConfig.GetRequestLocalizationOptions();
app.UseRequestLocalization(localizationOptions);

app.UseHttpsRedirection();
await app.MigrateDatabase();
app.MapControllers();

app.Run();
