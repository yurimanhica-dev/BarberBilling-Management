namespace BarberBilling.Communication.Requests.Billings.GetAll;

public class RequestGetAllBillingsJson
{
    // 🔍 Filtros
    public string? ClientName { get; set; }
    public string? ServiceName { get; set; }
    public decimal? AmountMin { get; set; }
    public decimal? AmountMax { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public int? Status { get; set; }

    // 📄 Paginação
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    // ↕️ Ordenação
    public string OrderBy { get; set; } = "ClientName";   // campo padrão
    public string Direction { get; set; } = "ASC";        // ASC ou DESC
}