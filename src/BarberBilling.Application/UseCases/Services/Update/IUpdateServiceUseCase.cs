using BarberBilling.Communication.Requests.Services;
using BarberBilling.Communication.Responses.Services;

namespace BarberBilling.Application.UseCases.Services.Update;

public interface IUpdateServiceUseCase
{
    Task<ResponseServiceJson> Execute(Guid serviceIdentifier, RequestServiceJson request);
}