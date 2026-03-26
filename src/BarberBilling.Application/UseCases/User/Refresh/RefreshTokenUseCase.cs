using BarberBilling.Communication.Responses.Auth;
using BarberBilling.Domain.Repositories;
using BarberBilling.Domain.Repositories.Token;
using BarberBilling.Domain.Repositories.User;
using BarberBilling.Domain.Security.Tokens;
using BarberBilling.Exceptions.ExceptionsBase;
using ExpenseManagement.Exception;
using Microsoft.Extensions.Localization;

namespace BarberBilling.Application.UseCases.User.Refresh;

public class RefreshTokenUseCase : IRefreshTokenUseCase
{
    private readonly IStringLocalizer<ErrorMessages> _localizer;
    private readonly IUserWriteOnlyRepository _userWriteOnlyRepository;
    private readonly ITokenReadOnlyRepository _tokenReadOnlyRepository;
    private readonly ITokenWriteOnlyRepository _tokenWriteOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;
    private readonly IAccessTokenGenerator _accessTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshTokenUseCase(
        ITokenReadOnlyRepository tokenReadOnlyRepository,
        IUserWriteOnlyRepository userWriteOnlyRepository,
        ITokenWriteOnlyRepository tokenWriteOnlyRepository,
        IUserReadOnlyRepository userReadOnlyRepository,
        IAccessTokenGenerator accessTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        IUnitOfWork unitOfWork, IStringLocalizer<ErrorMessages> localizer)
    {
        _tokenReadOnlyRepository = tokenReadOnlyRepository;
        _tokenWriteOnlyRepository = tokenWriteOnlyRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _accessTokenGenerator = accessTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _userWriteOnlyRepository = userWriteOnlyRepository;
        _localizer = localizer;
        _unitOfWork = unitOfWork;
    }
    public async Task<ResponseTokensJson> Execute(string refreshToken)
    {
        var token = await _tokenReadOnlyRepository.GetByValue(refreshToken)
            ?? throw new InvalidLoginException(_localizer["InvalidToken"].Value);

        if (token.ExpiresAt < DateTime.UtcNow)
        {
            await _tokenWriteOnlyRepository.DeleteAllByUserId(token.UserId);
            await _unitOfWork.Commit();
            throw new InvalidLoginException(_localizer["InvalidToken"].Value);
        }

        var user = await _userReadOnlyRepository.GetByIdentifier(token.UserId)
            ?? throw new InvalidLoginException(_localizer["InvalidToken"].Value);

        await _tokenWriteOnlyRepository.DeleteAllByUserId(token.UserId);
        
        var newAccessToken = _accessTokenGenerator.Generate(user);
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