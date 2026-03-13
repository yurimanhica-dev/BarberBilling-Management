namespace BarberBilling.Application.UseCases.Billings.Delete;

public interface IDeleteBillingUseCase
{
    Task Execute(Guid Id);
}