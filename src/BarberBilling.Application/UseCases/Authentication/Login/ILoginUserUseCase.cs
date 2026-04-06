using BarberBilling.Communication.Requests.Authentication.login;
using BarberBilling.Communication.Responses.Authentication;

namespace BarberBilling.Application.UseCases.Authentication.Login;

public interface ILoginUserUseCase
{
    public Task<ResponseTokensJson> Execute(RequestLoginJson request);
}