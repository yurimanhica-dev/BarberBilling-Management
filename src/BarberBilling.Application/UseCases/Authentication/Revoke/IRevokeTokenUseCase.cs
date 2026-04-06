namespace BarberBilling.Application.UseCases.Authentication.Revoke;

public interface IRevokeTokenUseCase
{
    Task Execute(Guid userId);
}