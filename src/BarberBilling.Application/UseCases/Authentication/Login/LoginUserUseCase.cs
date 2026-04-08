using BarberBilling.Application.Validators;
using BarberBilling.Communication.Requests.Authentication.login;
using BarberBilling.Communication.Responses.Authentication;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Token;
using BarberBilling.Domain.Repositories.User;
using BarberBilling.Domain.Repositories.User.Authorization;
using BarberBilling.Domain.Security.Cryptography;
using BarberBilling.Domain.Security.Tokens;
using BarberBilling.Exceptions.CustomExceptions;

namespace BarberBilling.Application.UseCases.Authentication.Login;

public class LoginUserUseCase : ILoginUserUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorizationReadOnlyRepository _authorizationRepository;
    private readonly IPasswordEncripte _passwordEncripte;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly ITokenWriteOnlyRepository _tokenWriteOnlyRepository;
    
    public LoginUserUseCase(IPasswordEncripte passwordEncripte, IUserReadOnlyRepository repository, IAccessTokenGenerator accessTokenGenerator, IRefreshTokenGenerator refreshTokenGenerator, IAuthorizationReadOnlyRepository authorizationRepository,
    ITokenWriteOnlyRepository tokenWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        _passwordEncripte = passwordEncripte;
        _userReadOnlyRepository = repository;
        _accessTokenGenerator = accessTokenGenerator;
        _authorizationRepository = authorizationRepository;
        _refreshTokenGenerator = refreshTokenGenerator;
        _tokenWriteOnlyRepository = tokenWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseTokensJson> Execute(RequestLoginJson request)
    {
        new LoginValidator().ValidateInput(request);

        var email = request.Email.ToLower().Trim();
        
        var user = await _userReadOnlyRepository.GetByEmail(email) 
            ?? throw new InvalidLoginException("EmailOrPasswordInvalid");

        var role = await _authorizationRepository.GetRoleByIdentifier(user.RoleIdentifier) 
            ?? throw new InvalidLoginException("EmailOrPasswordInvalid");

        var passwordMatch = _passwordEncripte.Verify(request.Password, user.Password);
        
        if(!passwordMatch)
        {
            throw new InvalidLoginException("EmailOrPasswordInvalid");
        }
        //Delete all old tokens
        await _tokenWriteOnlyRepository.DeleteAllByUserId(user.UserIdentifier);

        //Create new token
        var accessToken = _accessTokenGenerator.Generate(user, role);
        var refreshToken  = _refreshTokenGenerator.GenerateRefreshToken(user.UserIdentifier);

        await _tokenWriteOnlyRepository.SaveRefreshToken(refreshToken);

        await _unitOfWork.Commit();

        return new ResponseTokensJson
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };
    }
}