using System.Net.Mime;
using BarberBilling.Application.UseCases.Billings.Delete;
using BarberBilling.Application.UseCases.Billings.GetAll;
using BarberBilling.Application.UseCases.Billings.GetById;
using BarberBilling.Application.UseCases.Billings.Register;
using BarberBilling.Application.UseCases.Billings.Reports.Pdf;
using BarberBilling.Application.UseCases.Billings.Update;
using BarberBilling.Communication.Requests.Billings;
using BarberBilling.Communication.Responses;
using BarberBilling.Communication.Responses.Billings.GetById;
using BarberBilling.Exceptions.Domain;
using Microsoft.AspNetCore.Mvc;

namespace BarberBilling.Api.Controller;

[ApiController]
[Route("api/v1/billings")]
public class BillingController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BillingRequestJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterBilling(
        [FromServices] IRegisterBillingUseCase useCase,
        [FromBody] BillingRequestJson request)
    {
        var output = await useCase.Execute(request);
        return Created(string.Empty, output);
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ResponseBillingJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetBillings(
        [FromServices] IGetAllBillingUseCase useCase)
    {
        var response = await useCase.Execute();

        if (response is null)
            return NoContent();

        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseBillingJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBilling(
        [FromServices] IGetByIdBillingUseCase useCase,
        [FromRoute] Guid id)
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBilling(
        [FromServices] IUpdateBillingUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] BillingRequestJson request)
    {
        await useCase.Execute(id, request);
        return NoContent();
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteBilling(
        [FromServices] IDeleteBillingUseCase useCase,
        [FromRoute] Guid id)
    {
        await useCase.Execute(id);
        return NoContent();
    }

    [HttpGet("reports/weekly")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetWeeklyReport([FromServices] IGenerateBillingsReportPdfUseCase useCase, [FromQuery] DateOnly weekStart)
    {
        if (weekStart.DayOfWeek != DayOfWeek.Monday)
            throw new DomainException("WeekStartMustBeMonday");

        byte[] file = await useCase.ExecuteWeekly(weekStart);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, $"Corte_Fino_report_weekly_from_{weekStart}.pdf");

        return NoContent();
    }

    [HttpGet("reports/monthly")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetMonthlyReport([FromServices] IGenerateBillingsReportPdfUseCase useCase, [FromQuery] int year, [FromQuery] int month)
    {
        if (month < 1 || month > 12)
            throw new DomainException("InvalidMonth");

        byte[] file = await useCase.ExecuteMonthly(year, month);

        if (file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, $"Corte_Fino_report_monthly_{month}_from_{year}.pdf");

        return NoContent();
    }
}