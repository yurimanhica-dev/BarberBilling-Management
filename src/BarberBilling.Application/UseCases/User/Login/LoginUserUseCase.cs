using BarberBilling.Communication.Requests.Login;
using BarberBilling.Communication.Responses.Auth;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Token;
using BarberBilling.Domain.Repositories.User;
using BarberBilling.Domain.Security.Cryptography;
using BarberBilling.Domain.Security.Tokens;
using BarberBilling.Exceptions.ExceptionsBase;
using ExpenseManagement.Exception;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.UseCases.User.Login;

public class LoginUserUseCase : ILoginUserUseCase
{
    private readonly IStringLocalizer<ErrorMessages> _localizer;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordEncripte _passwordEncripte;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly ITokenWriteOnlyRepository _tokenWriteOnlyRepository;
    
    public LoginUserUseCase(IPasswordEncripte passwordEncripte, IUserReadOnlyRepository repository, IAccessTokenGenerator accessTokenGenerator, IRefreshTokenGenerator refreshTokenGenerator, ITokenWriteOnlyRepository tokenWriteOnlyRepository, IStringLocalizer<ErrorMessages> localizer, IUnitOfWork unitOfWork)
    {
        _passwordEncripte = passwordEncripte;
        _userReadOnlyRepository = repository;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _tokenWriteOnlyRepository = tokenWriteOnlyRepository;
        _localizer = localizer;
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseTokensJson> Execute(RequestLoginJson request)
    {
        var email = request.Email.ToLower().Trim();
        
        var user = await _userReadOnlyRepository.GetByEmail(email) 
            ?? throw new InvalidLoginException(_localizer["EmailOrPasswordInvalid"].Value);

        var passwordMatch = _passwordEncripte.Verify(request.Password, user.Password);
        
        if(!passwordMatch)
        {
            throw new InvalidLoginException(_localizer["EmailOrPasswordInvalid"].Value);
        }
        //Delete all old tokens
        await _tokenWriteOnlyRepository.DeleteAllByUserId(user.UserIdentifier);

        //Create new token
        var accessToken = _accessTokenGenerator.Generate(user);
        var refreshToken  = _refreshTokenGenerator.GenerateRefreshToken(user.UserIdentifier);

        // var refreshTokenValue = _passwordEncripte.Encrypt(refreshToken);

        await _tokenWriteOnlyRepository.SaveRefreshToken(refreshToken);

        await _unitOfWork.Commit();

        return new ResponseTokensJson
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token
        };
    }
}