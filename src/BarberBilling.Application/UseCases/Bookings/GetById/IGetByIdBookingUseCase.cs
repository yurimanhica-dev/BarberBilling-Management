using BarberBilling.Communication.Responses.Bookings.GetById;

namespace BarberBilling.Application.UseCases.Bookings.GetById;

public interface IGetByIdBookingUseCase
{
    Task<ResponseBookingJson> Execute(Guid id);
}
