namespace BarberBilling.Communication.Responses.Services.Delete;

public record ResponseSoftDeleteJson
(
    Guid Id,
    Communication.Enums.Services Services,
    bool IsDeleted
);