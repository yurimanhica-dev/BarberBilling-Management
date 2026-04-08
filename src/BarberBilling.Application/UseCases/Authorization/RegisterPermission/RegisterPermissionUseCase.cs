using BarberBilling.Application.Mappings;
using BarberBilling.Application.Validators;
using BarberBilling.Communication.Requests.Authorization;
using BarberBilling.Communication.Responses.Authorization;
using BarberBilling.Domain.Entities.Authorization;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.User.Authorization;
using BarberBilling.Exceptions.CustomExceptions;

namespace BarberBilling.Application.UseCases.Authorization.RegisterPermission;

public class RegisterPermissionUseCase : IRegisterPermissionUseCase
{
    private readonly IAuthorizationWriteOnlyRepository _writeRepository;
    private readonly IAuthorizationReadOnlyRepository _readRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterPermissionUseCase(
        IAuthorizationWriteOnlyRepository writeRepository,
        IAuthorizationReadOnlyRepository readRepository,
        IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponsePermissionJson> Execute(RequestRegisterPermissionJson request)
    {
        new PermissionValidator().ValidateInput(request);

        // Evita permissions duplicadas
        var exists = await _readRepository.GetPermissionByName(request.Name);

        if (exists is not null)
            throw new ConflictException("PermissionAlreadyExists");

        var permission = new Permission
        {
            PermissionIdentifier = Guid.NewGuid(),
            Name = request.Name.Trim().ToLower()
        };

        await _writeRepository.AddPermission(permission);
        await _unitOfWork.Commit();

        return permission.ToResponse();
    }
}