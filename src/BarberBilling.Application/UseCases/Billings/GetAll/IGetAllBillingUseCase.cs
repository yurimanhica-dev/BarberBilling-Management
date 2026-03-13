namespace BarberBilling.Application.UseCases.Billings.GetAll;

public interface IGetAllBillingUseCase
{
    Task<List<GetAllBillingOutput>> Execute();
}