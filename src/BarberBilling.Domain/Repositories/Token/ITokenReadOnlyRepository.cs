using BarberBilling.Domain.Entities.Login;

namespace BarberBilling.Domain.Repositories.Token;

public interface ITokenReadOnlyRepository
{
    Task<RefreshToken?> GetByValue(string value);
}