namespace BarberBilling.Communication.Responses.Bookings.BookingService;

public record ResponseBookingServiceJson
(
    Guid ServiceIdentifier,
    int ServiceType,
    string Name,
    decimal Price
);
