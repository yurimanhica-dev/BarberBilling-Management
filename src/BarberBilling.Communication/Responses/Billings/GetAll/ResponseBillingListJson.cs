

using BarberBilling.Communication.Enums;
using BarberBilling.Communication.Responses.Shared;

namespace BarberBilling.Communication.Responses.Billings.GetAll;

public record ResponseBillingListJson
(
    Guid Id,
    string ClientName,
    string ServiceName,
    decimal Amount,
    DateTimeOffset Date,
    EnumResponse Status
);
