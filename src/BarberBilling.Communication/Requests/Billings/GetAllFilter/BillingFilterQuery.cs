namespace BarberBilling.Communication.Requests.Billings.GetAllFilter;

public class BillingFilterQuery
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Status { get; set; } 
    public string SortBy { get; set; } = "createdAt";
    public string Order { get; set; } = "desc";
}