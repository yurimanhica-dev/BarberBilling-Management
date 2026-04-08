using BarberBilling.Communication.Requests.Users;
using Bogus;

namespace BarberBilling.Tests.CommonTestUtilities.Requests;

public class RequestRegisterUserJsonBuilder
{
    public static RequestRegisterUserJson Build()
    {
        return new Faker<RequestRegisterUserJson>()
            .RuleFor(u => u.Name, f => f.Person.FullName)
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
            .RuleFor(u => u.Password, f => f.Internet.Password(prefix: "!Aa1"))
            .RuleFor(u => u.RoleIdentifier, f => Guid.NewGuid());
    }
}