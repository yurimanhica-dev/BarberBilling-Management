using BarberBilling.Communication.Enums;

namespace BarberBilling.Communication.Requests.Billings;

public record BillingRequestJson
(
    DateTime Date,
    string BarberName,
    string ClientName,
    string ServiceName,
    decimal Amount,
    PaymentMethod PaymentMethod,
    Status Status,
    string? Notes
);