

using BarberBilling.Communication.Enums;
using BarberBilling.Communication.Responses.Shared;

namespace BarberBilling.Communication.Responses.Billings.GetById;

public class ResponseBillingJson
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string BarberName { get; set; } = string.Empty;
    public string ClientName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public EnumResponse PaymentMethod { get; set; } = new();
    public EnumResponse Status { get; set; } = new();
    public string? Notes { get; set; }
}