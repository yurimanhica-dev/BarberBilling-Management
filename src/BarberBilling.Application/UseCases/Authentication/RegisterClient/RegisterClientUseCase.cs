using BarberBilling.Application.Mappings;
using BarberBilling.Application.Validators;
using BarberBilling.Communication.Requests.Authentication.RegisterClient;
using BarberBilling.Communication.Responses.Authentication.RegisterClient;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.User;
using BarberBilling.Domain.Repositories.User.Authorization;
using BarberBilling.Domain.Security.Cryptography;
using BarberBilling.Domain.Security.Tokens;
using BarberBilling.Exceptions.CustomExceptions;
using ExpenseManagement.Exception;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.UseCases.User.Register;

public class RegisterClientUseCase : IRegisterClientUseCase
{
    private readonly IStringLocalizer<ErrorMessages> _localizer;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorizationReadOnlyRepository _authorizationRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordEncripte _passwordEncripte;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IAccessTokenGenerator  _accessTokenGenerator;
    public RegisterClientUseCase(IPasswordEncripte passwordEncripte, IUserReadOnlyRepository repository, IAuthorizationReadOnlyRepository authorizationRepository, IUserWriteOnlyRepository userWriteOnlyRepository, IStringLocalizer<ErrorMessages> localizer, IUnitOfWork unitOfWork, IAccessTokenGenerator accessTokenGenerator)
    {
        _passwordEncripte = passwordEncripte;
        _userReadOnlyRepository = repository;
        _authorizationRepository = authorizationRepository;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _localizer = localizer;
        _unitOfWork = unitOfWork;
        _accessTokenGenerator = accessTokenGenerator;
    }
    public async Task<ResponseRegisterClientJson> Execute(RequestRegisterClientJson request)
    {
        await Validate(request);

        var user = request.toEntity();

        var role = await _authorizationRepository.GetRoleByName("client");

        user.RoleIdentifier = role!.RoleIdentifier;
        user.Password = _passwordEncripte.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();
        

        await _userWriteOnlyRepository.Add(user);
        await _unitOfWork.Commit();

        return new ResponseRegisterClientJson
        {
            Name = user.Name,
            Token = _accessTokenGenerator.Generate(user, role)
        };
    }

    private async Task Validate(RequestRegisterClientJson request)
    {
        var result = new ClientValidator().Validate(request);
        
        var emailExists = await _userReadOnlyRepository.VerifyIfUserExist(request.Email);
        if (emailExists)
            result.Errors.Add(new ValidationFailure(string.Empty, _localizer["EmailAlreadyExists"].Value));

        if (!result.IsValid)
        {
            var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}