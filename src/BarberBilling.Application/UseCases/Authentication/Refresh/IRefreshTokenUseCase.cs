using BarberBilling.Communication.Responses.Authentication;

namespace BarberBilling.Application.UseCases.Authentication.Refresh;

public interface IRefreshTokenUseCase
{
    public Task<ResponseTokensJson> Execute(string refreshToken);
}