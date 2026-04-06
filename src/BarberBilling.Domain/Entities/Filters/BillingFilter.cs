using BarberBilling.Domain.Enums;

namespace BarberBilling.Domain.Entities.Filters;

public class BillingFilter
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public Status? Status { get; set; }
    public string Order { get; set; } = "desc";
    public string SortBy { get; set; } = "createdAt";
}