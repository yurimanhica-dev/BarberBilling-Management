using BarberBilling.Communication.Responses.Bookings.BookingService;
using BarberBilling.Communication.Responses.Shared;

namespace BarberBilling.Communication.Responses.Bookings.GetAll;

public record ResponseBookingListJson
(
    Guid Id,
    Guid ClientIdentifier,
    Guid BarberIdentifier,
    DateTime ScheduledDate,
    EnumResponse Status,
    decimal TotalAmount
);
