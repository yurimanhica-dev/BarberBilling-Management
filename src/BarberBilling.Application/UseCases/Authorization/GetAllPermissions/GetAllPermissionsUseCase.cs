using BarberBilling.Application.Mappings;
using BarberBilling.Communication.Responses.Authorization;
using BarberBilling.Domain.Repositories.User.Authorization;

namespace BarberBilling.Application.UseCases.Authorization.GetAllPermissions;

public class GetAllPermissionsUseCase : IGetAllPermissionsUseCase
{
    private readonly IAuthorizationReadOnlyRepository _repository;

    public GetAllPermissionsUseCase(IAuthorizationReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<ResponsePermissionJson>> Execute()
    {
        var permissions = await _repository.GetAllPermissions();
        return permissions.Select(p => p.ToResponse()).ToList();
    }
}