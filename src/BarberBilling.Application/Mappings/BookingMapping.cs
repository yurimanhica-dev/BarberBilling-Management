using BarberBilling.Application.Mappings.Common;
using BarberBilling.Application.Resources;
using BarberBilling.Communication.Requests.Bookings;
using BarberBilling.Communication.Responses.Bookings.BookingService;
using BarberBilling.Communication.Responses.Bookings.GetAll;
using BarberBilling.Communication.Responses.Bookings.GetById;
using BarberBilling.Communication.Responses.Bookings.Register;
using BarberBilling.Communication.Responses.Shared;
using BarberBilling.Domain.Entities.Bookings;
using BarberBilling.Domain.Entities.Filters;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.Mappings;

public static class BookingMapping
{
    public static Booking ToEntity(this BookingRequestJson request, Guid clientId)
    {
        return new Booking
        {
            Id = Guid.NewGuid(),
            ClientIdentifier = clientId,
            BarberIdentifier = request.BarberIdentifier,
            ScheduledDate = DateTime.SpecifyKind(request.ScheduledDate, DateTimeKind.Utc),
            Status = Domain.Enums.BookingStatus.Pending,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static BookingService ToServiceBookingEntity(this Domain.Entities.Service service, Guid bookingId)
    {
        return new BookingService
        {
            Id = Guid.NewGuid(),
            BookingId = bookingId,
            ServiceIdentifier = service.Id,
            ServiceType = service.Services,
            Price = service.Price
        };
    }

    public static ResponseRegisterBookingJson ToRegisterResponse(this Booking entity)
    {
        return new ResponseRegisterBookingJson(entity.Id);
    }

    public static ResponseBookingJson ToGetByIdResponse(this Booking entity, IStringLocalizer<ResourceEnumResponse> localizer)
    {
        return new ResponseBookingJson(
            entity.Id,
            entity.ClientIdentifier,
            entity.BarberIdentifier,
            entity.ScheduledDate,
            entity.TotalAmount,
            entity.Status.ToEnumResponse(localizer),
            entity.Notes,
            entity.Services.ToServicesResponse(localizer)
        );
    }

    public static List<ResponseBookingServiceJson> ToServicesResponse(this List<BookingService> services, IStringLocalizer<ResourceEnumResponse> localizer)
    {
        return services.Select(s => new ResponseBookingServiceJson(
            s.ServiceIdentifier,
            (int)s.ServiceType,
            s.ServiceType.ToEnumResponse(localizer).Description,
            s.Price
        )).ToList();
    }

    public static List<ResponseBookingListJson> ToGetAllResponse(this List<Booking> entities, IStringLocalizer<ResourceEnumResponse> localizer)
    {
        return entities.Select(entity => new ResponseBookingListJson(
            entity.Id,
            entity.ClientIdentifier,
            entity.BarberIdentifier,
            entity.ScheduledDate,
            entity.Status.ToEnumResponse(localizer),
            entity.Services.Sum(s => s.Price)
        )).ToList();
    }

    public static BookingFilter ToFilter(this Communication.Requests.Bookings.GetAllFilter.BookingFilterQuery query)
    {
        return new BookingFilter
        {
            Page = query.Page,
            PageSize = query.PageSize,
            Status = query.Status.HasValue ? (Domain.Enums.BookingStatus?)query.Status.Value : null,
            StartDate = query.StartDate,
            EndDate = query.EndDate,
            Order = query.Order,
            SortBy = query.SortBy
        };
    }
}
