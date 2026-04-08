using BarberBilling.Domain.Enums;

namespace BarberBilling.Domain.Entities.Bookings;

public class Booking
{
    public Guid Id { get; set; }
    public Guid ClientIdentifier { get; set; }
    public Guid BarberIdentifier { get; set; }
    public DateTime ScheduledDate { get; set; }
    public decimal TotalAmount { get; set; }
    public BookingStatus Status { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Serviços agendados — snapshot
    public List<BookingService> Services { get; set; } = [];
}
