using BarberBilling.Communication.Responses.Billings.GetAll;

namespace BarberBilling.Application.UseCases.Billings.GetAll;

public interface IGetAllBillingUseCase
{
    Task<ResponseBillingsJson> Execute();
}