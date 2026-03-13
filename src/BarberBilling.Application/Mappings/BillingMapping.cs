using BarberBilling.Application.UseCases.Billings;
using BarberBilling.Application.UseCases.Billings.GetAll;
using BarberBilling.Application.UseCases.Billings.GetById;
using BarberBilling.Application.UseCases.Billings.Register;
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
    public static BillingInput ToInput(this BillingRequestJson request)
    {
        return new BillingInput
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
    public static Billing ToEntity(this BillingInput input)
    {
        return new Billing
        {
            Date = input.Date,
            BarberName = input.BarberName,
            ClientName = input.ClientName,
            ServiceName = input.ServiceName,
            Amount = input.Amount,
            Status = input.Status,
            PaymentMethod = input.PaymentMethod,
            Notes = input.Notes
        };
    }
    public static RegisterBillingOutput ToRegisterOutput(this Billing entity)
    {
        return new RegisterBillingOutput
        {
            Id = entity.Id
        };
    }
    public static GetByIdBillingOutput ToGetByIdOutput(this Billing entity)
    {
        return new GetByIdBillingOutput
        {
            Id = entity.Id,
            Date = entity.Date,
            BarberName = entity.BarberName,
            ClientName = entity.ClientName,
            ServiceName = entity.ServiceName,
            Amount = entity.Amount,
            Status = entity.Status,
            PaymentMethod = entity.PaymentMethod,
            Notes = entity.Notes
        };
    }
    public static GetAllBillingOutput ToGetAllOutput(this Billing entity)
    {
        return new GetAllBillingOutput
        {
            Id = entity.Id,
            ClientName = entity.ClientName,
            ServiceName = entity.ServiceName,
            Amount = entity.Amount,
        };
    }
    public static List<GetAllBillingOutput> ToGetAllOutputs(this List<Billing> entities)
    {
        return entities.Select(entity => entity.ToGetAllOutput()).ToList();
    }
    // Register
    public static ResponseRegisterBillingJson ToResponse(this RegisterBillingOutput output)
    {
        return new ResponseRegisterBillingJson
        {
            Id = output.Id
        };
    }
    // GetById
    public static ResponseBillingJson ToResponse(this GetByIdBillingOutput output)
    {
        return new ResponseBillingJson
        {
            Id = output.Id,
            Date = output.Date,
            BarberName = output.BarberName,
            ClientName = output.ClientName,
            ServiceName = output.ServiceName,
            Amount = output.Amount,
            PaymentMethod = new EnumResponse
            {
                Id = (int)output.PaymentMethod,
                Description = output.PaymentMethod.ToString()
            },
            Status = new EnumResponse
            {
                Id = (int)output.Status,
                Description = output.Status.ToString()
            },
            Notes = output.Notes
        };
    }
    // GetAll
    public static List<ResponseBillingListJson> ToResponse(this List<GetAllBillingOutput> outputs)
    {
        return outputs.Select(output => new ResponseBillingListJson
        {
            Id = output.Id,
            ClientName = output.ClientName,
            ServiceName = output.ServiceName,
            Amount = output.Amount
        }).ToList();
    }
    public static Billing UpdateEntity(this BillingInput input, Billing billing)
    {
        billing.Date = input.Date;
        billing.BarberName = input.BarberName;
        billing.ClientName = input.ClientName;
        billing.ServiceName = input.ServiceName;
        billing.Amount = input.Amount;
        billing.PaymentMethod = input.PaymentMethod;
        billing.Notes = input.Notes;
        billing.UpdatedAt = DateTime.UtcNow;
        return billing;
    }
}