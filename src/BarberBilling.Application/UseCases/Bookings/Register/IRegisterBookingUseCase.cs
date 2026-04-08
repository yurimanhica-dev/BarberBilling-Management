using BarberBilling.Communication.Requests.Bookings;
using BarberBilling.Communication.Responses.Bookings.Register;

namespace BarberBilling.Application.UseCases.Bookings.Register;

public interface IRegisterBookingUseCase
{
    Task<ResponseRegisterBookingJson> Execute(BookingRequestJson request, Guid clientId);
}
