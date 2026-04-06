namespace BarberBilling.Communication.Requests.Services.GetAllFilter;

public class ServiceFilterQuery
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public bool? IsDeleted { get; set; }
    public string SortBy { get; set; } = "createdAt";
    public string Order { get; set; } = "desc";
    public int? Category { get; set; } 
}