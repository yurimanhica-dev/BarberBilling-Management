namespace BarberBilling.Communication.Responses.Authorization;

public class ResponseRoleJson
{
    public Guid RoleIdentifier { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> Permissions { get; set; } = [];
}