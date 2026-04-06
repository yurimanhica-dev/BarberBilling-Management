using BarberBilling.Communication.Requests.Authentication;
using BarberBilling.Communication.Requests.Authentication.RegisterClient;
using BarberBilling.Domain.Entities;

namespace BarberBilling.Application.Mappings;

public static class AuthenticationMapping
{
    public static User toEntity(this RequestRegisterClientJson request)
    {
        return new User
        {
            Name = request.Name,
            Email = request.Email.ToLower().Trim(),
            CreatedAt = DateTime.UtcNow
        };
    }
}