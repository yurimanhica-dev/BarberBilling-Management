using BarberBilling.Communication.Requests.Bookings;
using Bogus;

namespace BarberBilling.Tests.CommonTestUtilities.Requests;

public class BookingRequestJsonBuilder
{
    public static BookingRequestJson Build()
    {
        return new Faker<BookingRequestJson>()
            .CustomInstantiator(f => new BookingRequestJson(
                ScheduledDate: DateTime.UtcNow.AddDays(f.Random.Int(1, 30)),
                BarberIdentifier: Guid.NewGuid(),
                ServiceIds: new List<Guid>
                {
                    Guid.NewGuid(),
                    Guid.NewGuid()
                },
                Notes: f.Lorem.Sentence()
            ))
            .Generate();
    }
}
