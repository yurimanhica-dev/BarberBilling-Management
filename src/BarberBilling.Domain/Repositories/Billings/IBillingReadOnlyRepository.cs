using BarberBilling.Domain.Entities.Billings;
using BarberBilling.Domain.Entities.Filters;
using BarberBilling.Domain.Enums;

namespace BarberBilling.Domain.Repositories.Billings;

public interface IBillingReadOnlyRepository
{
    Task<(List<Billing> Items, int TotalCount)> GetAll(BillingFilter filter, Guid userId, string role);
    Task<Billing?> GetById(Guid id);
    Task<List<Billing>> GetByRange(DateOnly start, DateOnly end, Status? status = null);
}