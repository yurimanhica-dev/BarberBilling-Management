namespace BarberBilling.Application.UseCases.User.Revoke;

public interface IRevokeTokenUseCase
{
    Task Execute(Guid userId);
}