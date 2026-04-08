namespace BarberBilling.Tests.CommonTestUtilities.helpers;

public  static class RequestLongAndShortName
{
    public static IEnumerable<object[]> LongNames =>
    [
        [new string('J', 102)],
        [new string('A', 150)]
    ];

    public static IEnumerable<object[]> ShortNames =>
    [
        [new string('J', 1)],
        [new string('A', 2)]
    ];
}
