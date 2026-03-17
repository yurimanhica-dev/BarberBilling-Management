namespace BarberBilling.Application.UseCases.Billings.Reports.Pdf;

public interface IGenerateBillingsReportPdfUseCase
{
    Task<byte[]> ExecuteWeekly(DateOnly weekStart);
    Task<byte[]> ExecuteMonthly(int year, int month);
}
