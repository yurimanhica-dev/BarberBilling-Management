using BarberBilling.Domain.Entities.Bookings;

namespace BarberBilling.Domain.Repositories.Bookings;

public interface IBookingWriteOnlyRepository
{
    Task Add(Booking booking);
    Task Update(Booking booking);
    Task<bool> Delete(Guid id);
}
