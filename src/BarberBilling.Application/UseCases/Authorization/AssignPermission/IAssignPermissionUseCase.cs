using BarberBilling.Communication.Requests.Authorization;

namespace BarberBilling.Application.UseCases.Authorization.AssignPermission;

public interface IAssignPermissionUseCase
{
    Task Execute(Guid roleId, RequestPermissionsJson request);
}