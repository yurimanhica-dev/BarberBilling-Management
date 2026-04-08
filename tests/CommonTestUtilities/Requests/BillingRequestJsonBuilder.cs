using BarberBilling.Communication.Enums;
using BarberBilling.Communication.Requests.Billings;
using Bogus;

namespace BarberBilling.Tests.CommonTestUtilities.Requests;

public class BillingRequestJsonBuilder
{
    public static BillingRequestJson Build()
    {
        return new Faker<BillingRequestJson>()
            .CustomInstantiator(f => new BillingRequestJson(
                Date: DateTime.UtcNow.AddDays(-f.Random.Int(1, 10)),
                ClientIdentifier: Guid.NewGuid(),
                ServiceIds: new List<Guid>
                {
                    Guid.NewGuid(),
                    Guid.NewGuid()
                },
                PaymentMethod: f.PickRandom<PaymentMethod>(),
                Status: f.PickRandom<Status>(),
                Notes: f.Lorem.Sentence()
            ))
            .Generate();
    }
}
