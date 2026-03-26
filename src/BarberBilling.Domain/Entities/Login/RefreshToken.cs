namespace BarberBilling.Domain.Entities.Login;

public class RefreshToken
{
    public long Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public Guid UserId { get; set; }
}