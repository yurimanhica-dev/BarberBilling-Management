using BarberBilling.Communication.Requests.Authorization;

namespace BarberBilling.Application.UseCases.Authorization.RevokePermission;

public interface IRevokePermissionUseCase
{
    Task Execute(Guid roleId, RequestPermissionsJson request);
}