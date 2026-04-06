namespace BarberBilling.Communication.Responses.User.Register;

public class ResponseRegisterUserJson
{
    public Guid UserIdentifier { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}