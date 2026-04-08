using BarberBilling.Application.Mappings;
using BarberBilling.Communication.Requests.Users;
using BarberBilling.Communication.Responses.User.Register;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.User;
using BarberBilling.Domain.Repositories.User.Authorization;
using BarberBilling.Domain.Security.Cryptography;
using BarberBilling.Exceptions.CustomExceptions;
using ExpenseManagement.Exception;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.UseCases.User;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserWriteOnlyRepository _writeRepository;
    private readonly IUserReadOnlyRepository _readRepository;
    private readonly IStringLocalizer<ErrorMessages> _localizer;
    private readonly IPasswordEncripte _passwordEncripte;
    private readonly IAuthorizationReadOnlyRepository _authorizationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserUseCase(
        IUserWriteOnlyRepository writeRepository,
        IUserReadOnlyRepository readRepository,
        IStringLocalizer<ErrorMessages> localizer,
        IPasswordEncripte passwordEncripte,
        IAuthorizationReadOnlyRepository authorizationRepository,
        IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _localizer = localizer;
        _passwordEncripte = passwordEncripte;
        _authorizationRepository = authorizationRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        // Role definida pelo Admin
        var role = await _authorizationRepository.GetRoleByIdentifier(request.RoleIdentifier)
            ?? throw new NotFoundException("EmailAlreadyExists");

        var user = request.ToEntity();
        user.RoleIdentifier = role.RoleIdentifier;
        user.Password = _passwordEncripte.Encrypt(request.Password);

        await _writeRepository.Add(user);
        await _unitOfWork.Commit();

        return user.ToRegisterUserResponse(role);
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new UserValidator().Validate(request);
        
        var emailExists = await _readRepository.VerifyIfUserExist(request.Email);
        if (emailExists)
            result.Errors.Add(new ValidationFailure(string.Empty, _localizer["EmailAlreadyExists"].Value));

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}