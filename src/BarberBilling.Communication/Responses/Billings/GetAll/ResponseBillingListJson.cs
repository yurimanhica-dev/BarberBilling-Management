using BarberBilling.Communication.Responses.Shared;

namespace BarberBilling.Communication.Responses.Billings.GetAll;

public record ResponseBillingListJson
(
    Guid Id,
    Guid ClientIdentifier,       
    decimal TotalAmount,          
    DateTimeOffset Date,
    EnumResponse Status
);