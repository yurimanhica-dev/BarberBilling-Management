using BarberBilling.Application.Mappings;
using BarberBilling.Communication.Requests.Authorization;
using BarberBilling.Communication.Responses.Authorization;
using BarberBilling.Domain.Entities.Authorization;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.User.Authorization;
using BarberBilling.Exceptions.CustomExceptions;

namespace BarberBilling.Application.UseCases.Authorization.RegisterRole;

public class RegisterRoleUseCase : IRegisterRoleUseCase
{
    private readonly IAuthorizationWriteOnlyRepository _writeRepository;
    private readonly IAuthorizationReadOnlyRepository _readRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterRoleUseCase(
        IAuthorizationWriteOnlyRepository writeRepository,
        IAuthorizationReadOnlyRepository readRepository,
        IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRoleJson> Execute(RequestCreateRoleJson request)
    {
        // Evita roles duplicadas
        var exists = await _readRepository.GetRoleByName(request.Name);

        if (exists is not null)
            throw new ConflictException("Role já existe.");

        //TODO: INSERIR LOCALIZER
        var role = new Role
        {
            RoleIdentifier = Guid.NewGuid(),
            Name = request.Name.Trim()
        };

        await _writeRepository.AddRole(role);
        await _unitOfWork.Commit();

        return role.ToResponseRegisterRole();
    }
}