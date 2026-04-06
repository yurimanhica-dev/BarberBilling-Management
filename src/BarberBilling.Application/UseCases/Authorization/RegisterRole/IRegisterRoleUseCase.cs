using BarberBilling.Communication.Requests.Authorization;
using BarberBilling.Communication.Responses.Authorization;

namespace BarberBilling.Application.UseCases.Authorization.RegisterRole;

public interface IRegisterRoleUseCase
{
    Task<ResponseRoleJson> Execute(RequestCreateRoleJson request);
}