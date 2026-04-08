using BarberBilling.Application.Validators;
using BarberBilling.Communication.Requests.Authorization;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.User.Authorization;
using BarberBilling.Exceptions.CustomExceptions;

namespace BarberBilling.Application.UseCases.Authorization.RevokePermission;

public class RevokePermissionUseCase : IRevokePermissionUseCase
{
    private readonly IAuthorizationReadOnlyRepository _readRepository;
    private readonly IAuthorizationWriteOnlyRepository _writeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RevokePermissionUseCase(
        IAuthorizationReadOnlyRepository readRepository,
        IAuthorizationWriteOnlyRepository writeRepository,
        IUnitOfWork unitOfWork)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid roleId, RequestPermissionsJson request)
    {
        new PermissionsRequestValidator().ValidateInput(request);

        var removed = 0;

        foreach (var permissionId in request.PermissionIds)
        {
            var rolePermission = await _readRepository.GetRolePermission(roleId, permissionId);

            if (rolePermission is null)
                continue;

            await _writeRepository.RevokePermission(rolePermission);
            removed++;
        }

        if (removed == 0)
            throw new NotFoundException("RolePermissionNotFound");

        await _unitOfWork.Commit();
    }
}