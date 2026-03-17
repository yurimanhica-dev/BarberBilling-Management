using BarberBilling.Application.Settings;
using BarberBilling.Application.UseCases.Billings.Delete;
using BarberBilling.Application.UseCases.Billings.GetAll;
using BarberBilling.Application.UseCases.Billings.GetById;
using BarberBilling.Application.UseCases.Billings.Register;
using BarberBilling.Application.UseCases.Billings.Reports.Pdf;
using BarberBilling.Application.UseCases.Billings.Update;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBilling.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
    }

    private static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<IRegisterBillingUseCase, RegisterBillingUseCase>();
        services.AddScoped<IGetAllBillingUseCase, GetAllBillingUseCase>();
        services.AddScoped<IGetByIdBillingUseCase, GetByIdBillingUseCase>();
        services.AddScoped<IDeleteBillingUseCase, DeleteBillingUseCase>();
        services.AddScoped<IUpdateBillingUseCase, UpdateBillingUseCase>();
        services.AddScoped<IGenerateBillingsReportPdfUseCase, GenerateBillingsReportPdfUseCase>();
    }
}