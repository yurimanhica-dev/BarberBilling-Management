using BarberBilling.Domain.Enums;

namespace BarberBilling.Domain.Entities.Billings;

public class Billing
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Guid BarberIdentifier { get; set; }
    public Guid ClientIdentifier { get; set; }
    public List<BillingService> Services { get; set; } = [];
    public decimal TotalAmount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public Status Status { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}