namespace BarberBilling.Domain.Repositories.User;

public interface IUserWriteOnlyRepository
{
    Task Add(Entities.User user);
    Task Update(Entities.User user);
}