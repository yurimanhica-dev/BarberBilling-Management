namespace BarberBilling.Application.UseCases.Authorization.RevokePermission;

public interface IRevokePermissionUseCase
{
    Task Execute(Guid roleId, List<Guid> permissionIds);
}