namespace BarberBilling.Domain.Security.Tokens;


public interface IAccessTokenGenerator
{
    string Generate(Entities.User user);
}