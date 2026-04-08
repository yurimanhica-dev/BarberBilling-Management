using BarberBilling.Application.Mappings;
using BarberBilling.Application.Resources;
using BarberBilling.Communication.Responses.Bookings.GetById;
using BarberBilling.Domain.Repositories.Bookings;
using BarberBilling.Exceptions.CustomExceptions;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.UseCases.Bookings.GetById;

public class GetByIdBookingUseCase : IGetByIdBookingUseCase
{
    private readonly IBookingReadOnlyRepository _bookingRepository;
    private readonly IStringLocalizer<ResourceEnumResponse> _statusLocalizer;

    public GetByIdBookingUseCase(
        IBookingReadOnlyRepository bookingRepository,
        IStringLocalizer<ResourceEnumResponse> statusLocalizer)
    {
        _bookingRepository = bookingRepository;
        _statusLocalizer = statusLocalizer;
    }

    public async Task<ResponseBookingJson> Execute(Guid id)
    {
        var result = await _bookingRepository.GetById(id) ?? throw new NotFoundException("BookingNotFound");

        var response = result.ToGetByIdResponse(_statusLocalizer);

        return response;
    }
}
