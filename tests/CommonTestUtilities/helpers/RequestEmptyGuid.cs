namespace BarberBilling.Tests.CommonTestUtilities.helpers;

public static class RequestEmptyGuid
{
    public static IEnumerable<object[]> EmptyGuids =>
    [
        [Guid.Empty],
    ];
}
