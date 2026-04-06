
namespace BarberBilling.Domain.Entities.Authorization;

public class RolePermissions
{
    public int Id { get; set; }
    public Guid RoleIdentifier { get; set; }
    public Role Role { get; set; } = null!;
    public Guid PermissionIdentifier { get; set; }
    public Permission Permission { get; set; } = null!;
}