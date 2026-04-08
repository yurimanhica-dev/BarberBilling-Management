using BarberBilling.Communication.Responses.Bookings.GetAll;

namespace BarberBilling.Communication.Responses.Bookings.GetAll;

public class ResponseBookingsJson
{
    public List<ResponseBookingListJson> Bookings { get; set; } = [];
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
}
