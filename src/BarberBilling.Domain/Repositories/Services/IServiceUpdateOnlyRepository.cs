using BarberBilling.Domain.Entities;

namespace BarberBilling.Domain.Repositories.Services;

public interface IServiceUpdateOnlyRepository
{
    Task<Service?> GetByIdentifier(Guid serviceIdentifier);
    Task Update(Service service);
}