using BarberBilling.Domain.Enums;

namespace BarberBilling.Domain.Entities.QueryParameters;

public class BillingFilter
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 5;
    public Status? Status { get; set; }
    public string Order { get; set; } = "desc";
    public string SortBy { get; set; } = "createdAt";
}