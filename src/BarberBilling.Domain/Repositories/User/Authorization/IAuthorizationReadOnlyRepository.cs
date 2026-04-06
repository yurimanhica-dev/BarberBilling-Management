using BarberBilling.Domain.Entities.Authorization;

namespace BarberBilling.Domain.Repositories.User.Authorization;

public interface IAuthorizationReadOnlyRepository
{
    Task<List<Role>> GetAllRoles();
    Task<List<Permission>> GetAllPermissions();
    Task<RolePermissions?> GetRolePermission(Guid roleIdentifier, Guid permissionIdentifier);
    Task<Role?> GetRoleByIdentifier(Guid roleIdentifier);
    Task<Permission?> GetPermissionByName(string name);
    Task<Role?> GetRoleByName(string name);
}