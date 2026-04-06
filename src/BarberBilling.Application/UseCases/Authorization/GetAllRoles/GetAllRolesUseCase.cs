using BarberBilling.Application.Mappings;
using BarberBilling.Communication.Responses.Authorization;
using BarberBilling.Domain.Repositories.User.Authorization;

namespace BarberBilling.Application.UseCases.Authorization.GetAllRoles;

public class GetAllRolesUseCase : IGetAllRolesUseCase
{
    private readonly IAuthorizationReadOnlyRepository _repository;

    public GetAllRolesUseCase(IAuthorizationReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ResponseRoleJson>> Execute()
    {
        var roles = await _repository.GetAllRoles();
        return roles.Select(r => r.ToResponseGetRole()).ToList();
    }
}