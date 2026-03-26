using BarberBilling.Domain.Entities.Login;

namespace BarberBilling.Domain.Repositories.Token;

public interface ITokenWriteOnlyRepository
{
    Task SaveRefreshToken(RefreshToken refreshToken);
    Task DeleteAllByUserId(Guid userId);
}