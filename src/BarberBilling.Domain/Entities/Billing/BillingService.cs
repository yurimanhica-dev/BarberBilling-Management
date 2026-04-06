using BarberBilling.Domain.Enums;

namespace BarberBilling.Domain.Entities.Billings;

public class BillingService
{
    public Guid Id { get; set; }
    public Guid BillingId { get; set; }
    public Guid ServiceIdentifier { get; set; }
    public Services ServiceType { get; set; }
    public decimal Price { get; set; }
}