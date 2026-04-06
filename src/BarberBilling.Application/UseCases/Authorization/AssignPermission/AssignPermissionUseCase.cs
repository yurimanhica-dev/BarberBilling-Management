using BarberBilling.Communication.Requests.Authorization;
using BarberBilling.Domain.Entities.Authorization;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.User.Authorization;
using BarberBilling.Exceptions.CustomExceptions;

namespace BarberBilling.Application.UseCases.Authorization.AssignPermission;

public class AssignPermissionUseCase : IAssignPermissionUseCase
{
    private readonly IAuthorizationReadOnlyRepository _readRepository;
    private readonly IAuthorizationWriteOnlyRepository _writeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AssignPermissionUseCase(
        IAuthorizationReadOnlyRepository readRepository,
        IAuthorizationWriteOnlyRepository writeRepository,
        IUnitOfWork unitOfWork)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid roleId, List<Guid> permissionIds)
    {
        var removed = 0;

        foreach (var permissionId in permissionIds)
        {
            // Evita duplicados
            var exists = await _readRepository.GetRolePermission(roleId, permissionId);

            if (exists is not null)
                continue;

            await _writeRepository.AssignPermission(new RolePermissions
            {
                RoleIdentifier = roleId,
                PermissionIdentifier = permissionId
            });
            removed++;
        }

        if (removed == 0)
            throw new NotFoundException("PermissionAlreadyExists");

        await _unitOfWork.Commit();
    }
}