using BarberBilling.Communication.Responses.Authorization;

namespace BarberBilling.Application.UseCases.Authorization.GetAllPermissions;

public interface IGetAllPermissionsUseCase
{
    Task<List<ResponsePermissionJson>> Execute();
}