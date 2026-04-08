namespace BarberBilling.Tests.CommonTestUtilities.helpers;
public  static class RequestInvalidAmounts
{
    public static IEnumerable<object[]> InvalidAmounts()
    {           
        yield return new object[] { 0 };
        yield return new object[] { -0.01m };
        yield return new object[] { -1 };
        yield return new object[] { -100 };
    }
}