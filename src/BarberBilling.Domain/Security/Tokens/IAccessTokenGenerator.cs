using BarberBilling.Domain.Entities.Authorization;

namespace BarberBilling.Domain.Security.Tokens;


public interface IAccessTokenGenerator
{
    string Generate(Entities.User user, Role role);
}