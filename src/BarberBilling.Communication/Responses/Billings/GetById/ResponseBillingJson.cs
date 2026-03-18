using BarberBilling.Communication.Enums;
using BarberBilling.Communication.Responses.Shared;

namespace BarberBilling.Communication.Responses.Billings.GetById;

public record ResponseBillingJson
(
    Guid Id,
    DateTime Date,
    string BarberName,
    string ClientName,
    string ServiceName,
    decimal Amount,
    EnumResponse PaymentMethod,
    EnumResponse Status,
    string? Notes
);