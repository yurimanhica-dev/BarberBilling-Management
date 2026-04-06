using System.Net.Mime;
using BarberBilling.Api.Security.Authorization;
using BarberBilling.Application.Mappings;
using BarberBilling.Application.UseCases.Billings.Reports.Pdf;
using BarberBilling.Communication.Responses;
using BarberBilling.Exceptions.CustomExceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberBilling.Api.Controller;

[Route("api/v1/reports")]
[ApiController]
public class ReportsController : ControllerBase
{
    [HttpGet("billings/weekly")]
    [Authorize(Policy = Permissions.Reports.ReadBillings)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetWeeklyReport(
        [FromServices] IGenerateBillingsReportPdfUseCase useCase,
        [FromQuery] DateOnly weekStart,
        [FromQuery] string? status = null)
    {
        if (weekStart.DayOfWeek != DayOfWeek.Monday)
            throw new DomainException("WeekStartMustBeMonday");

        byte[] file = await useCase.ExecuteWeekly(weekStart, status.ToStatus());

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, $"Corte Fino reporte semanal de {weekStart} a {weekStart.AddDays(7)}.pdf");

        return NoContent();
    }

    [HttpGet("billings/monthly")]
    [Authorize(Policy = Permissions.Reports.ReadBillings)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMonthlyReport([FromServices] IGenerateBillingsReportPdfUseCase useCase, [FromQuery] int year, [FromQuery] int month)
    {
        if (month < 1 || month > 12)
            throw new DomainException("InvalidMonth");

        byte[] file = await useCase.ExecuteMonthly(year, month);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, $"Corte Fino reporte mensal {month} de {year}.pdf");

        return NoContent();
    }
}