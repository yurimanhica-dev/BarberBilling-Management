using BarberBilling.Communication.Enums;

namespace BarberBilling.Communication.Requests.Billings;

public class BillingRequestJson
{
    public DateTime Date { get; set; }
    public string BarberName { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public Status Status { get; set; }
    public string? Notes { get; set; }
}