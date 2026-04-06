using BarberBilling.Domain.Entities.Authorization;

namespace BarberBilling.Domain.Entities;

public class User
{
    public long Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } =  string.Empty;
    public Guid UserIdentifier { get; set; }
    // FK
    public Guid RoleIdentifier { get; set; }

    // Navegação
    public Role? Role { get; set; }
    public int TokenVersion { get; set; } = 1;
    public DateTime CreatedAt { get; set; }
}