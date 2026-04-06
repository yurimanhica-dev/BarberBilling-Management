using BarberBilling.Communication.Responses.Billings.GetAll;
using BarberBilling.Domain.Entities.Filters;

namespace BarberBilling.Application.UseCases.Billings.GetAll;

public interface IGetAllBillingUseCase
{
    Task<ResponseBillingsJson> Execute(BillingFilter filter, Guid userId, string role);
}