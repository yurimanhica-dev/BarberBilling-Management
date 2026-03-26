using BarberBilling.Communication.Requests.Billings;
using BarberBilling.Communication.Responses.Billings.Register;

namespace BarberBilling.Application.UseCases.Billings.Register;

public interface IRegisterBillingUseCase
{
    Task<ResponseRegisterBillingJson> Execute(BillingRequestJson request, Guid userId);
}