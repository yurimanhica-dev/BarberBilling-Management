using BarberBilling.Domain.Entities.Bookings;
using BarberBilling.Domain.Entities.Filters;

namespace BarberBilling.Domain.Repositories.Bookings;

public interface IBookingReadOnlyRepository
{
    Task<(List<Booking> Items, int TotalCount)> GetAll(BookingFilter filter, Guid userId, string role);
    Task<Booking?> GetById(Guid id);
    Task<List<Booking>> GetByClient(Guid clientId);
    Task<List<Booking>> GetByBarber(Guid barberId);
    Task<List<Booking>> GetByDateRange(DateOnly start, DateOnly end);
}
