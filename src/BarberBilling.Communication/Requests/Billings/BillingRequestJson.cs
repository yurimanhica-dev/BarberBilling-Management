using BarberBilling.Communication.Enums;

namespace BarberBilling.Communication.Requests.Billings;

public record BillingRequestJson(
    DateTime Date,
    Guid ClientIdentifier,
    List<Guid> ServiceIds,
    PaymentMethod PaymentMethod,
    Status Status,
    string? Notes
);