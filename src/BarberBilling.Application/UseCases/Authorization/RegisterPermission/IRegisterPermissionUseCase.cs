using BarberBilling.Communication.Requests.Authorization;
using BarberBilling.Communication.Responses.Authorization;

namespace BarberBilling.Application.UseCases.Authorization.RegisterPermission;

public interface IRegisterPermissionUseCase
{
    Task<ResponsePermissionJson> Execute(RequestRegisterPermissionJson request);
}
