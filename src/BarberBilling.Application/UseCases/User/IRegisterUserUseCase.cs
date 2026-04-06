using BarberBilling.Communication.Requests.Users;
using BarberBilling.Communication.Responses.User.Register;

namespace BarberBilling.Application.UseCases.User;

public interface IRegisterUserUseCase
{
    Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request);
}