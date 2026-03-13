using BarberBilling.Domain.Entities;

namespace BarberBilling.Domain.Repositories.Billings;

public interface IBillingReadOnlyRepository
{
    Task<List<Billing>> GetAll();
    Task<Billing?> GetById(Guid id);
}