using BarberBilling.Communication.Enums;

namespace BarberBilling.Communication.Requests.Bookings;

public record BookingRequestJson(
    DateTime ScheduledDate,
    Guid BarberIdentifier,
    List<Guid> ServiceIds,
    string? Notes
);
