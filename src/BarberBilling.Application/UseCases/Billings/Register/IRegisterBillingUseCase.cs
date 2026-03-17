using BarberBilling.Communication.Requests.Billings;
using BarberBilling.Communication.Responses.Billings.Register;
using BarberBilling.Domain.Entities;

namespace BarberBilling.Application.UseCases.Billings.Register;

public interface IRegisterBillingUseCase
{
    Task<ResponseRegisterBillingJson> Execute(BillingRequestJson request);
}