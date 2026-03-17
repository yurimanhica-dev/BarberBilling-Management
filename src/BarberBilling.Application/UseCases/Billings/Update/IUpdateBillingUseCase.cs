using BarberBilling.Communication.Requests.Billings;

namespace BarberBilling.Application.UseCases.Billings.Update;

public interface IUpdateBillingUseCase
{
    Task Execute(Guid id, BillingRequestJson request);
}