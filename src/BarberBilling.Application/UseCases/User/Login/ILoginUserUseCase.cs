using BarberBilling.Communication.Requests.Login;
using BarberBilling.Communication.Responses.Auth;

namespace BarberBilling.Application.UseCases.User.Login;

public interface ILoginUserUseCase
{
    public Task<ResponseTokensJson> Execute(RequestLoginJson request);
}