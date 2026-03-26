using BarberBilling.Communication.Responses.Auth;

namespace BarberBilling.Application.UseCases.User.Refresh;

public interface IRefreshTokenUseCase
{
    public Task<ResponseTokensJson> Execute(string refreshToken);
}