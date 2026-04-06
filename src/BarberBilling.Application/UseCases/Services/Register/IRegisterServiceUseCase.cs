using BarberBilling.Communication.Requests.Services;
using BarberBilling.Communication.Responses.Services;
using BarberBilling.Communication.Responses.Services.Register;

namespace BarberBilling.Application.UseCases.Services.Register;

public interface IRegisterServiceUseCase
{
    Task<ResponseRegisterServiceJson> Execute(RequestServiceJson request);
}