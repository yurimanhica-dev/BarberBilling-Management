using BarberBilling.Communication.Requests.Authentication;
using BarberBilling.Communication.Requests.Authentication.RegisterClient;
using BarberBilling.Communication.Responses.Authentication.RegisterClient;
namespace BarberBilling.Application.UseCases.User.Register;

public interface IRegisterClientUseCase
{
    Task<ResponseRegisterClientJson> Execute(RequestRegisterClientJson request);
}