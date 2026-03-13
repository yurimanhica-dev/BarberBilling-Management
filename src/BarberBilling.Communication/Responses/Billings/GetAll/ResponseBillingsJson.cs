namespace BarberBilling.Communication.Responses.Billings.GetAll;

public class ResponseBillingsJson
{
    public List<ResponseBillingListJson> Billings { get; set; } = [];
    // TODO: Add paging and filter by date and status./
}