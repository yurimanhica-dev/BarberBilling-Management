namespace BarberBilling.Communication.Responses.Services;

public record ResponseServiceJson
(
    Guid ServiceIdentifier,
    int Id,
    string Description,
    decimal Price
);