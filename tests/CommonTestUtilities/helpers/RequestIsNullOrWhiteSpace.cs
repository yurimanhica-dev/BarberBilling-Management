namespace BarberBilling.Tests.CommonTestUtilities.helpers;

public static class RequestIsNullOrWhiteSpace
{
    public static IEnumerable<object[]> InvalidValues()
    {
        yield return new object[] { string.Empty };
        yield return new object[] { " " };
        yield return new object[] { "   " };
        yield return new object[] { null! };
    }
}