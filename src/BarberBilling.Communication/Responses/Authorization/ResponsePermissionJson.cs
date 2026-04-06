namespace BarberBilling.Communication.Responses.Authorization;

public class ResponsePermissionJson
{
    public Guid PermissionIdentifier { get; set; }
    public string Name { get; set; } = string.Empty;
}