using BarberBilling.Domain.Entities.Login;

namespace BarberBilling.Domain.Security.Tokens;

public interface IRefreshTokenGenerator
{
    RefreshToken GenerateRefreshToken(Guid userId);
}