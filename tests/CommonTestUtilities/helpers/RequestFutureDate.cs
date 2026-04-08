namespace BarberBilling.Tests.CommonTestUtilities.helpers;

public static class RequestDate
{
    public static IEnumerable<object[]> FutureDates =>
    [
        [DateTime.UtcNow.AddDays(1)],
        [DateTime.UtcNow.AddDays(10)],
        [DateTime.UtcNow.AddMonths(1)],
    ];

    public static IEnumerable<object[]> PastDates =>
    [
        [DateTime.UtcNow.AddDays(-1)],
        [DateTime.UtcNow.AddDays(-10)],
        [DateTime.UtcNow.AddMonths(-1)],
    ];
}
