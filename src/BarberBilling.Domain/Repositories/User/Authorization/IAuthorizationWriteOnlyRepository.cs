using BarberBilling.Domain.Entities.Authorization;

namespace BarberBilling.Domain.Repositories.User.Authorization;

public interface IAuthorizationWriteOnlyRepository
{
    Task AssignPermission(RolePermissions rolePermission);
    Task RevokePermission(RolePermissions rolePermission);
    Task AddRole(Role role);
    Task AddPermission(Permission permission);
}