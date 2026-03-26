using BarberBilling.Application.Resources;
using BarberBilling.Application.UseCases.Billings.Delete;
using BarberBilling.Application.UseCases.Billings.GetAll;
using BarberBilling.Application.UseCases.Billings.GetById;
using BarberBilling.Application.UseCases.Billings.Register;
using BarberBilling.Application.UseCases.Billings.Reports.Pdf;
using BarberBilling.Application.UseCases.Billings.Update;
using BarberBilling.Application.UseCases.User.Login;
using BarberBilling.Application.UseCases.User.Refresh;
using BarberBilling.Application.UseCases.User.Register;
using BarberBilling.Application.UseCases.User.Revoke;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
    }
    private static void AddUseCases(IServiceCollection services)
    {
        // Billing
        services.AddScoped<IRegisterBillingUseCase, RegisterBillingUseCase>();
        services.AddScoped<IGetAllBillingUseCase, GetAllBillingUseCase>();
        services.AddScoped<IGetByIdBillingUseCase, GetByIdBillingUseCase>();
        services.AddScoped<IStringLocalizer<ResourceEnumResponse>, StringLocalizer<ResourceEnumResponse>>();
        services.AddScoped<IDeleteBillingUseCase, DeleteBillingUseCase>();
        services.AddScoped<IUpdateBillingUseCase, UpdateBillingUseCase>();
        services.AddScoped<IGenerateBillingsReportPdfUseCase, GenerateBillingsReportPdfUseCase>();

        // User
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        
        // Login
        services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();

        // RefreshToken
        services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();

        // Logout
        services.AddScoped<IRevokeTokenUseCase, RevokeTokenUseCase>();
    }
}