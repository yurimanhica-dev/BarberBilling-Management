using BarberBilling.Communication.Responses.Billings.BillingService;
using BarberBilling.Communication.Responses.Shared;

namespace BarberBilling.Communication.Responses.Billings.GetById;

public record ResponseBillingJson
(
    Guid Id,
    DateTime Date,
    Guid BarberIdentifier,
    Guid ClientIdentifier,
    decimal TotalAmount,
    EnumResponse PaymentMethod,
    EnumResponse Status,
    string? Notes,
    List<ResponseBillingServiceJson> Services
);