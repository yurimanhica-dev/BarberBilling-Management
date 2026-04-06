using BarberBilling.Application.Mappings.Common;
using BarberBilling.Application.Resources;
using BarberBilling.Communication.Requests.Billings;
using BarberBilling.Communication.Requests.Billings.GetAllFilter;
using BarberBilling.Communication.Responses.Billings.BillingService;
using BarberBilling.Communication.Responses.Billings.GetAll;
using BarberBilling.Communication.Responses.Billings.GetById;
using BarberBilling.Communication.Responses.Billings.Register;
using BarberBilling.Domain.Entities;
using BarberBilling.Domain.Entities.Billings;
using BarberBilling.Domain.Entities.Filters;
using BarberBilling.Domain.Enums;
using Microsoft.Extensions.Localization;

public static class BillingMapping
{
     public static Status? ToStatus(this string? status)
    {
        return Enum.TryParse<Status>(status, out var s) ? s : null;
    }
    public static Billing ToEntity(this BillingRequestJson request)
    {
        return new Billing
        {
            Id = Guid.NewGuid(),
            Date = DateTime.SpecifyKind(request.Date, DateTimeKind.Utc),
            Status = (Status)request.Status,
            PaymentMethod = (PaymentMethod)request.PaymentMethod,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
    }

    public static BillingService ToServiceEntity(this Service service, Guid billingId)
    {
        return new BillingService
        {
            Id = Guid.NewGuid(),
            BillingId = billingId,
            ServiceIdentifier = service.Id,
            ServiceType = service.Services, 
            Price = service.Price 
        };
    }

    public static ResponseRegisterBillingJson ToRegisterResponse(this Billing entity)
    {
        return new ResponseRegisterBillingJson(entity.Id);
    }

    public static ResponseBillingJson ToGetByIdResponse(this Billing entity, IStringLocalizer<ResourceEnumResponse> localizer)
    {
        return new ResponseBillingJson(
            entity.Id,
            entity.Date,
            entity.BarberIdentifier,
            entity.ClientIdentifier,
            entity.TotalAmount,
            entity.PaymentMethod.ToEnumResponse(localizer),
            entity.Status.ToEnumResponse(localizer),
            entity.Notes,
            entity.Services.ToServicesResponse(localizer)
        );
    }

    public static List<ResponseBillingServiceJson> ToServicesResponse(this List<BillingService> services, IStringLocalizer<ResourceEnumResponse> localizer)
    {
        return services.Select(s => new ResponseBillingServiceJson(
            s.ServiceIdentifier,
            (int)s.ServiceType,
            s.ServiceType.ToEnumResponse(localizer).Description,
            s.Price
        )).ToList();
    }

    public static List<ResponseBillingListJson> ToGetAllResponse(this List<Billing> entities, IStringLocalizer<ResourceEnumResponse> localizer)
    {
        return entities.Select(entity => new ResponseBillingListJson(
            entity.Id,
            entity.ClientIdentifier,         
            entity.TotalAmount,              
            entity.Date,
            entity.Status.ToEnumResponse(localizer)
        )).ToList();
    }

    public static BillingFilter ToFilter(this BillingFilterQuery query)
    {
        return new BillingFilter
        {
            Page = query.Page,
            PageSize = query.PageSize,
            Status = Enum.TryParse<Status>(query.Status, out var status) ? status : null,
            Order = query.Order,
            SortBy = query.SortBy
        };
    }

    public static Billing UpdateEntity(this BillingRequestJson request, Billing billing)
    {
        billing.Date = DateTime.SpecifyKind(request.Date, DateTimeKind.Utc);
        billing.PaymentMethod = (PaymentMethod)request.PaymentMethod;
        billing.Status = (Status)request.Status;
        billing.Notes = request.Notes;
        billing.UpdatedAt = DateTime.UtcNow;
        return billing;
    }
}