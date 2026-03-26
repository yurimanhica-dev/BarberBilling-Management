using BarberBilling.Communication.Responses.Billings.GetAll;
using BarberBilling.Domain.Entities.QueryParameters;

namespace BarberBilling.Application.UseCases.Billings.GetAll;

public interface IGetAllBillingUseCase
{
    Task<ResponseBillingsJson> Execute(BillingFilter filter, Guid userId, string role);
}