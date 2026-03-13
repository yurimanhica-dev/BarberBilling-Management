using BarberBilling.Domain.Entities;

namespace BarberBilling.Domain.Repositories.Billings;

public interface IBillingUpdateOnlyRepository
{
    Task<Billing?> GetById(Guid id);
    void Update(Billing billing);
}