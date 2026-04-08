using BarberBilling.Communication.Responses.Bookings.GetAll;
using BarberBilling.Domain.Entities.Filters;

namespace BarberBilling.Application.UseCases.Bookings.GetAll;

public interface IGetAllBookingUseCase
{
    Task<ResponseBookingsJson> Execute(BookingFilter filter, Guid userId, string role);
}
