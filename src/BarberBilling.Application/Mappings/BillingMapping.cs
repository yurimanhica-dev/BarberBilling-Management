using BarberBilling.Communication.Requests.Billings;
using BarberBilling.Communication.Responses.Billings.GetAll;
using BarberBilling.Communication.Responses.Billings.GetById;
using BarberBilling.Communication.Responses.Billings.Register;
using BarberBilling.Communication.Responses.Shared;
using BarberBilling.Domain.Entities;
using BarberBilling.Domain.Enums;

namespace BarberBilling.Application.Mappings;

public static class BillingMapping
{
    public static Billing ToEntity(this BillingRequestJson request)
    {
        return new Billing
        {
            Date = request.Date,
            BarberName = request.BarberName,
            ClientName = request.ClientName,
            ServiceName = request.ServiceName,
            Amount = request.Amount,
            Status = (Status)request.Status,
            PaymentMethod = (PaymentMethod)request.PaymentMethod,
            Notes = request.Notes
        };
    }
    public static ResponseRegisterBillingJson ToRegisterResponse(this Billing entity)
    {
        return new ResponseRegisterBillingJson
        {
            Id = entity.Id
        };
    }
    public static ResponseBillingJson ToGetByIdResponse(this Billing entity)
    {
        return new ResponseBillingJson
        {
            Id = entity.Id,
            Date = entity.Date,
            BarberName = entity.BarberName,
            ClientName = entity.ClientName,
            ServiceName = entity.ServiceName,
            Amount = entity.Amount,
            PaymentMethod = new EnumResponse
            {
                Id = (int)entity.PaymentMethod,
                Description = entity.PaymentMethod.ToString()
            },
            Status = new EnumResponse
            {
                Id = (int)entity.Status,
                Description = entity.Status.ToString()
            },
            Notes = entity.Notes
        };
    }
    public static List<ResponseBillingListJson> ToGetAllResponse(this List<Billing> entities)
    {
        return entities.Select(entity => new ResponseBillingListJson
        {
            Id = entity.Id,
            ClientName = entity.ClientName,
            ServiceName = entity.ServiceName,
            Amount = entity.Amount
        }).ToList();
    }
    public static ResponseBillingsJson ToGetAllOutputs(this List<Billing> entities)
    {
        return new ResponseBillingsJson
        {
            Billings = entities.ToGetAllResponse()
            
        };
    }
    public static Billing UpdateEntity(this BillingRequestJson request, Billing billing)
    {
        billing.Date = request.Date;
        billing.BarberName = request.BarberName;
        billing.ClientName = request.ClientName;
        billing.ServiceName = request.ServiceName;
        billing.Amount = request.Amount;
        billing.PaymentMethod = (PaymentMethod)request.PaymentMethod;
        billing.Notes = request.Notes;
        billing.UpdatedAt = DateTime.UtcNow;
        return billing;
    }
}