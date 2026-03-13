namespace BarberBilling.Application.UseCases.Billings.GetAll;

public class GetAllBillingOutput
{
    public Guid Id { get; set; }
    public string ClientName { get; set; } = string.Empty;
    public string ServiceName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}