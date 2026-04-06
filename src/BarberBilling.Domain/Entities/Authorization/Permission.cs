namespace BarberBilling.Domain.Entities.Authorization;

public class Permission
{
    public int Id { get; set; }
    public Guid PermissionIdentifier { get; set; }
    public string Name { get; set; } = string.Empty; // "services:create"
    public List<RolePermissions> RolePermissions { get; set; } = [];
}