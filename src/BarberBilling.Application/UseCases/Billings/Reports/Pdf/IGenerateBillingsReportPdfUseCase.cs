

using BarberBilling.Domain.Enums;

namespace BarberBilling.Application.UseCases.Billings.Reports.Pdf;

public interface IGenerateBillingsReportPdfUseCase
{
    Task<byte[]> ExecuteWeekly(DateOnly weekStart, Status? status = null);
    Task<byte[]> ExecuteMonthly(int year, int month);
}
