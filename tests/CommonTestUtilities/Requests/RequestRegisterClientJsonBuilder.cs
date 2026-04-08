using BarberBilling.Communication.Requests.Authentication.RegisterClient;
using Bogus;

namespace BarberBilling.Tests.CommonTestUtilities.Requests;

public class RequestRegisterClientJsonBuilder
{
    public static RequestRegisterClientJson Build()
    {
        return new Faker<RequestRegisterClientJson>()
            .RuleFor(u => u.Name, f => f.Person.FullName)
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name))
            .RuleFor(u => u.Password, f => f.Internet.Password(prefix: "!Aa1"));
    }
}
