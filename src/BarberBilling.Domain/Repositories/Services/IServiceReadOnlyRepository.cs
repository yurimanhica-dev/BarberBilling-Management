using BarberBilling.Domain.Entities;
using BarberBilling.Domain.Entities.Filters;

namespace BarberBilling.Domain.Repositories.Services;

public interface IServiceReadOnlyRepository
{
    Task<(List<Service> Items, int TotalCount)> GetAll(ServiceFilter filter);
    Task<Service?> GetByIdentifier(Guid serviceIdentifier);
    Task<Service?> GetByName(string name);
}