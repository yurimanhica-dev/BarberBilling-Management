using BarberBilling.Communication.Responses.Bookings.BookingService;
using BarberBilling.Communication.Responses.Shared;

namespace BarberBilling.Communication.Responses.Bookings.GetById;

public record ResponseBookingJson
(
    Guid Id,
    Guid ClientIdentifier,
    Guid BarberIdentifier,
    DateTime ScheduledDate,
    decimal TotalAmount,
    EnumResponse Status,
    string? Notes,
    List<ResponseBookingServiceJson> Services
);
