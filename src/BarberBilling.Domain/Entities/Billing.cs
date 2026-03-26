using BarberBilling.Domain.Enums;

namespace BarberBilling.Domain.Entities;

public class Billing
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Guid BarberIdentifier { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public Status Status { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}