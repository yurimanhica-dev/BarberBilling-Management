using BarberBilling.Communication.Responses.Authentication;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Token;
using BarberBilling.Domain.Repositories.User;
using BarberBilling.Domain.Repositories.User.Authorization;
using BarberBilling.Domain.Security.Tokens;
using BarberBilling.Exceptions.CustomExceptions;
using ExpenseManagement.Exception;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.UseCases.Authentication.Refresh;

public class RefreshTokenUseCase : IRefreshTokenUseCase
{
    private readonly ITokenReadOnlyRepository _tokenReadOnlyRepository;
    private readonly ITokenWriteOnlyRepository _tokenWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly  IAuthorizationReadOnlyRepository _authorizationRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenUseCase(
        ITokenReadOnlyRepository tokenReadOnlyRepository,
        ITokenWriteOnlyRepository tokenWriteOnlyRepository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IAuthorizationReadOnlyRepository authorizationRepository,
        IAccessTokenGenerator accessTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        IUnitOfWork unitOfWork, IStringLocalizer<ErrorMessages> localizer)
    {
        _tokenReadOnlyRepository = tokenReadOnlyRepository;
        _tokenWriteOnlyRepository = tokenWriteOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _authorizationRepository = authorizationRepository;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseTokensJson> Execute(string refreshToken)
    {
        var token = await _tokenReadOnlyRepository.GetByValue(refreshToken)
            ?? throw new InvalidLoginException("InvalidToken");

        if (token.ExpiresAt < DateTime.UtcNow)
        {
            await _tokenWriteOnlyRepository.DeleteAllByUserId(token.UserId);
            await _unitOfWork.Commit();
            throw new InvalidLoginException("InvalidToken");
        }

        var user = await _userReadOnlyRepository.GetByIdentifier(token.UserId)
            ?? throw new InvalidLoginException("InvalidToken");
            
        var role = await _authorizationRepository.GetRoleByIdentifier(user.RoleIdentifier)
            ?? throw new InvalidLoginException("InvalidToken");

        await _tokenWriteOnlyRepository.DeleteAllByUserId(token.UserId);
        
        var newAccessToken = _accessTokenGenerator.Generate(user, role);
        var newRefreshToken = _refreshTokenGenerator.GenerateRefreshToken(user.UserIdentifier);
        
        await _tokenWriteOnlyRepository.SaveRefreshToken(newRefreshToken);
        await _unitOfWork.Commit();

        return new ResponseTokensJson
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token
        };
    }
}