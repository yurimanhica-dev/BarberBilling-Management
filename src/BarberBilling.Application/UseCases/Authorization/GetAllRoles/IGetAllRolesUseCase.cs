using BarberBilling.Communication.Responses.Authorization;

namespace BarberBilling.Application.UseCases.Authorization.GetAllRoles;

public interface IGetAllRolesUseCase
{
    Task<List<ResponseRoleJson>> Execute();
}