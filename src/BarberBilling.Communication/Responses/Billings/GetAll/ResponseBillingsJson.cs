namespace BarberBilling.Communication.Responses.Billings.GetAll;

public class ResponseBillingsJson
{
    public List<ResponseBillingListJson> Billings { get; set; } = [];
    // public int Page { get; set; }
    // public int PageSize { get; set; }
    // public int TotalCount { get; set; }
    // public int TotalPages { get; set; }
}