using BarberBilling.Communication.Requests.Users;
using BarberBilling.Communication.Responses.User.Register;
using BarberBilling.Domain.Entities;
using BarberBilling.Domain.Security.Tokens;

namespace BarberBilling.Application.Mappings;

public static class UserMapping
{
    public static User ToEntity(this RequestRegisterUserJson request)
    {
        return new User
        {
            Name = request.Name,
            Email = request.Email.ToLower().Trim()
        };
    }
}