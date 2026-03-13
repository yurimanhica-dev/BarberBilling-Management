using BarberBilling.Domain.Entities;

namespace BarberBilling.Domain.Repositories.Billings;

public interface IBillingWriteOnlyRepository
{
    Task Add(Billing billing);
    /// <summary>
    /// This functions returns true if the billing was deleted
    /// </summary>
    /// <param name="billingId"></param>
    /// <returns></returns>
    Task<bool> Delete(Guid Id);
}