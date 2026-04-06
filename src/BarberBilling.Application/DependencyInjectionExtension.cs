using BarberBilling.Application.Resources;
using BarberBilling.Application.UseCases.Authentication.Login;
using BarberBilling.Application.UseCases.Authentication.Refresh;
using BarberBilling.Application.UseCases.Authentication.Revoke;
using BarberBilling.Application.UseCases.Authorization.AssignPermission;
using BarberBilling.Application.UseCases.Authorization.GetAllPermissions;
using BarberBilling.Application.UseCases.Authorization.GetAllRoles;
using BarberBilling.Application.UseCases.Authorization.RegisterPermission;
using BarberBilling.Application.UseCases.Authorization.RegisterRole;
using BarberBilling.Application.UseCases.Authorization.RevokePermission;
using BarberBilling.Application.UseCases.Billings.Delete;
using BarberBilling.Application.UseCases.Billings.GetAll;
using BarberBilling.Application.UseCases.Billings.GetById;
using BarberBilling.Application.UseCases.Billings.Register;
using BarberBilling.Application.UseCases.Billings.Reports.Pdf;
using BarberBilling.Application.UseCases.Billings.Update;
using BarberBilling.Application.UseCases.Categories.GetAll;
using BarberBilling.Application.UseCases.Services.Delete;
using BarberBilling.Application.UseCases.Services.GetAll;
using BarberBilling.Application.UseCases.Services.Register;
using BarberBilling.Application.UseCases.User;
using BarberBilling.Application.UseCases.User.Register;
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
        
        //Client
        services.AddScoped<IRegisterClientUseCase, RegisterClientUseCase>();

        // Login
        services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();

        // RefreshToken
        services.AddScoped<IRefreshTokenUseCase, RefreshTokenUseCase>();

        // Logout
        services.AddScoped<IRevokeTokenUseCase, RevokeTokenUseCase>();

        //Authorization
        services.AddScoped<IRegisterPermissionUseCase, RegisterPermissionUseCase>();
        services.AddScoped<IGetAllPermissionsUseCase, GetAllPermissionsUseCase>();
        services.AddScoped<IRegisterRoleUseCase, RegisterRoleUseCase>();
        services.AddScoped<IGetAllRolesUseCase, GetAllRolesUseCase>();
        services.AddScoped<IRevokePermissionUseCase, RevokePermissionUseCase>();
        services.AddScoped<IAssignPermissionUseCase, AssignPermissionUseCase>();
        
        //Services
        services.AddScoped<IRegisterServiceUseCase, RegisterServiceUseCase>();
        services.AddScoped<IGetAllServicesUseCase, GetAllServicesUseCase>();
        services.AddScoped<IDeleteServiceUseCase, DeleteServiceUseCase>();

        // Categories
        services.AddScoped<IGetAllCategoriesUseCase, GetAllCategoriesUseCase>();
    }
}