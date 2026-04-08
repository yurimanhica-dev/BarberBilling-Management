using BarberBilling.Domain.Enums;

namespace BarberBilling.Domain.Entities.Bookings;

public class BookingService
{
    public Guid Id { get; set; }
    public Guid BookingId { get; set; }
    public Guid ServiceIdentifier { get; set; }
    public Services ServiceType { get; set; }
    public decimal Price { get; set; }
}
