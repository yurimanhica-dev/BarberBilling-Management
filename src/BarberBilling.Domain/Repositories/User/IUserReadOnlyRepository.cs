namespace BarberBilling.Domain.Repositories.User;

public interface IUserReadOnlyRepository
{
    Task<bool> VerifyIfUserExist(string email);
    Task<Entities.User?> GetByEmail(string email);
    Task<Entities.User?> GetByIdentifier(Guid userIdentifier);
}