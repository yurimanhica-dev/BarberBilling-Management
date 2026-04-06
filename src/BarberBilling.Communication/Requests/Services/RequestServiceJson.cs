using BarberBilling.Communication.Enums;

namespace BarberBilling.Communication.Requests.Services;

public class RequestServiceJson
{
    public Communication.Enums.Services Services { get; set; }
    public decimal Price { get; set; }
    public Category Category { get; set; }
}