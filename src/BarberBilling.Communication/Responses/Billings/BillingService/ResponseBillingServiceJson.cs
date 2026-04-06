namespace BarberBilling.Communication.Responses.Billings.BillingService;

public record ResponseBillingServiceJson
(
    Guid ServiceIdentifier,
    int ServiceType,
    string Name,   
    decimal Price
);