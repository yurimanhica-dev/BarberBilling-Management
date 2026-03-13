namespace BarberBilling.Application.UseCases.Billings.GetById;

public interface IGetByIdBillingUseCase
{
    Task<GetByIdBillingOutput> Execute(Guid id);
}