using BarberBilling.Communication.Requests.Users;
using BarberBilling.Communication.Responses.User.Register;
using BarberBilling.Domain.Entities;
using BarberBilling.Domain.Entities.Authorization;

namespace BarberBilling.Application.Mappings;

public static class UserMapping
{
    public static ResponseRegisterUserJson ToRegisterUserResponse(this User user, Role role)
    {
        return new ResponseRegisterUserJson
        {
            UserIdentifier = user.UserIdentifier,
            Name = user.Name,
            Email = user.Email,
            Role = role.Name
        };
    }

    public static User ToEntity(this RequestRegisterUserJson request)
    {
        return new User
        {
            UserIdentifier = Guid.NewGuid(),
            Name = request.Name.Trim(),
            Email = request.Email.ToLower().Trim(),
            TokenVersion = 1,
            CreatedAt = DateTime.UtcNow
        };
    }
}