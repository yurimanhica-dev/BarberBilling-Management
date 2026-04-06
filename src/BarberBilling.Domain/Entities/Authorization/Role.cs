namespace BarberBilling.Domain.Entities.Authorization;

public class Role
{
    public int Id { get; set; }
    public Guid RoleIdentifier { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<RolePermissions> RolePermissions { get; set; } = [];
}