namespace BarberBilling.Communication.Requests.Authorization;

public class RequestPermissionsJson
{
    public List<Guid> PermissionIds { get; set; } = [];
}