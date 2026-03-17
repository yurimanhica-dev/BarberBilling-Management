using BarberBilling.Communication.Responses.Billings.GetById;

namespace BarberBilling.Application.UseCases.Billings.GetById;

public interface IGetByIdBillingUseCase
{
    Task<ResponseBillingJson> Execute(Guid id);
}