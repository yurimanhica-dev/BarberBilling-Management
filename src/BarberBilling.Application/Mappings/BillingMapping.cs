using BarberBilling.Application.Mappings.Common;
using BarberBilling.Application.Resources;
using BarberBilling.Communication.Requests.Billings;
using BarberBilling.Communication.Responses.Billings.GetAll;
using BarberBilling.Communication.Responses.Billings.GetById;
using BarberBilling.Communication.Responses.Billings.Register;
using BarberBilling.Domain.Entities;
using BarberBilling.Domain.Enums;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.Mappings;

public static class BillingMapping
{
    public static Billing ToEntity(this BillingRequestJson request)
    {
        return new Billing
        {
            Date = DateTime.SpecifyKind(request.Date, DateTimeKind.Utc),
            BarberName = request.BarberName,
            ClientName = request.ClientName,
            ServiceName = request.ServiceName,
            Amount = request.Amount,
            Status = (Status)request.Status,
            PaymentMethod = (PaymentMethod)request.PaymentMethod,
            Notes = request.Notes,
            CreatedAt = DateTime.UtcNow
        };
    }
    public static ResponseRegisterBillingJson ToRegisterResponse(this Billing entity)
    {
        return new ResponseRegisterBillingJson
        (
        entity.Id
        );
    }
    public static ResponseBillingJson ToGetByIdResponse(this Billing entity, IStringLocalizer<ResourceEnumResponse> localizer)
    {
        return new ResponseBillingJson(
            entity.Id,
            entity.Date,
            entity.BarberName,
            entity.ClientName,
            entity.ServiceName,
            entity.Amount,
            entity.PaymentMethod.ToEnumResponse(localizer),
            entity.Status.ToEnumResponse(localizer),
            entity.Notes
        );
    }
    public static List<ResponseBillingListJson> ToGetAllResponse(this List<Billing> entities, IStringLocalizer<ResourceEnumResponse>? localizer)
    {
        return entities.Select(entity => new ResponseBillingListJson(
            entity.Id,
            entity.ClientName,
            entity.ServiceName,
            entity.Amount,
            entity.Date,
            entity.Status.ToEnumResponse(localizer!)
        )).ToList();
    }
    public static ResponseBillingsJson ToGetAllOutputs(this List<Billing> entities)
    {
        return new ResponseBillingsJson
        {
            Billings = entities.ToGetAllResponse(
                localizer: null
            )
        };
    }
    public static Billing UpdateEntity(this BillingRequestJson request, Billing billing)
    {
        billing.Date = DateTime.SpecifyKind(request.Date, DateTimeKind.Utc);
        billing.BarberName = request.BarberName;
        billing.ClientName = request.ClientName;
        billing.ServiceName = request.ServiceName;
        billing.Amount = request.Amount;
        billing.PaymentMethod = (PaymentMethod)request.PaymentMethod;
        billing.Status = (Status)request.Status;
        billing.Notes = request.Notes;
        billing.UpdatedAt = DateTime.UtcNow;
        return billing;
    }
}