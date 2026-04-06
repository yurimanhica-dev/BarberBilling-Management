using BarberBilling.Domain.Enums;

namespace BarberBilling.Domain.Entities.Filters;

public class ServiceFilter
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public bool IsDeleted { get; set; } = false;
    public string Order { get; set; } = "desc";
    public string SortBy { get; set; } = "createdAt";
    public int? Category { get; set; }
}