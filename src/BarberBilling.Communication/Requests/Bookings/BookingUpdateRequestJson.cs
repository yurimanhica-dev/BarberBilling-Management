using BarberBilling.Communication.Enums;

namespace BarberBilling.Communication.Requests.Bookings;

public record BookingUpdateRequestJson(
    BookingStatus Status,
    string? Notes
);
