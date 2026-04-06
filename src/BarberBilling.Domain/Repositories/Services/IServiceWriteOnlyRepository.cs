using BarberBilling.Domain.Entities;

namespace BarberBilling.Domain.Repositories.Services;

public interface IServiceWriteOnlyRepository
{
    Task Add(Service service);
    Task SoftDelete(Service service, bool isDeleted);
}