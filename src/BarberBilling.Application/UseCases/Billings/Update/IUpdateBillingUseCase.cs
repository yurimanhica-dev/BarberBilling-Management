namespace BarberBilling.Application.UseCases.Billings.Update;

public interface IUpdateBillingUseCase
{
    Task Execute(Guid id, BillingInput input);
}