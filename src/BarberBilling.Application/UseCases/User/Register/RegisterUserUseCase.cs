using BarberBilling.Application.Mappings;
using BarberBilling.Communication.Requests.Users;
using BarberBilling.Communication.Responses.User.Register;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.User;
using BarberBilling.Domain.Security.Cryptography;
using BarberBilling.Domain.Security.Tokens;
using BarberBilling.Exceptions.ExceptionsBase;
using ExpenseManagement.Exception;
using FluentValidation.Results;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.UseCases.User.Register;

public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IStringLocalizer<ErrorMessages> _localizer;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IPasswordEncripte _passwordEncripte;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly IAccessTokenGenerator  _accessTokenGenerator;
    public RegisterUserUseCase(IPasswordEncripte passwordEncripte, IUserReadOnlyRepository repository, IStringLocalizer<ErrorMessages> localizer, IUserWriteOnlyRepository userWriteOnlyRepository, IUnitOfWork unitOfWork, IAccessTokenGenerator accessTokenGenerator)
    {
        _passwordEncripte = passwordEncripte;
        _userReadOnlyRepository = repository;
        _localizer = localizer;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _accessTokenGenerator = accessTokenGenerator;
    }
    public async Task<ResponseRegisterUserJson> Execute(RequestRegisterUserJson request)
    {
        await Validate(request);

        var user = request.ToEntity();

        user.Password = _passwordEncripte.Encrypt(request.Password);
        user.UserIdentifier = Guid.NewGuid();

        await _userWriteOnlyRepository.Add(user);
        await _unitOfWork.Commit();

        return new ResponseRegisterUserJson
        {
            Name = user.Name,
            Token = _accessTokenGenerator.Generate(user)
        };
    }

    private async Task Validate(RequestRegisterUserJson request)
    {
        var result = new UserValidator(_localizer).Validate(request);
        
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