using BarberBilling.Application.Mappings;
using BarberBilling.Application.Resources;
using BarberBilling.Communication.Responses.Bookings.GetAll;
using BarberBilling.Domain.Entities.Filters;
using BarberBilling.Domain.Repositories.Bookings;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.UseCases.Bookings.GetAll;

public class GetAllBookingUseCase : IGetAllBookingUseCase
{
    private readonly IBookingReadOnlyRepository _bookingRepository;
    private readonly IStringLocalizer<ResourceEnumResponse> _statusLocalizer;

    public GetAllBookingUseCase(
        IBookingReadOnlyRepository bookingRepository,
        IStringLocalizer<ResourceEnumResponse> statusLocalizer)
    {
        _bookingRepository = bookingRepository;
        _statusLocalizer = statusLocalizer;
    }

    public async Task<ResponseBookingsJson> Execute(BookingFilter filter, Guid userId, string role)
    {
        var (bookings, totalCount) = await _bookingRepository.GetAll(filter, userId, role);

        return new ResponseBookingsJson
        {
            Bookings = bookings.ToGetAllResponse(_statusLocalizer),
            TotalCount = totalCount,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalPages = (int)Math.Ceiling((double)totalCount / filter.PageSize)
        };
    }
}
