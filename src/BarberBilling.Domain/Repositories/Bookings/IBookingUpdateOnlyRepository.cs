using BarberBilling.Domain.Entities.Bookings;

namespace BarberBilling.Domain.Repositories.Bookings;

public interface IBookingUpdateOnlyRepository
{
    Task Update(Booking booking);
}
