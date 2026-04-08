using BarberBilling.Communication.Requests.Bookings;

namespace BarberBilling.Application.UseCases.Bookings.Update;

public interface IUpdateBookingUseCase
{
    Task Execute(Guid id, BookingUpdateRequestJson request);
}
