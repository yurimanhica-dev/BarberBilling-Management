using BarberBilling.Communication.Enums;

namespace BarberBilling.Communication.Requests.Bookings.GetAllFilter;

public class BookingFilterQuery
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public BookingStatus? Status { get; set; }
    public DateOnly? StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public string? Order { get; set; } = "desc";
    public string? SortBy { get; set; } = "ScheduledDate";
}
