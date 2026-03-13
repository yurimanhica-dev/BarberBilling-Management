using BarberBilling.Application.Mappings;
using BarberBilling.Application.UseCases.Billings;
using BarberBilling.Application.UseCases.Billings.Delete;
using BarberBilling.Application.UseCases.Billings.GetAll;
using BarberBilling.Application.UseCases.Billings.GetById;
using BarberBilling.Application.UseCases.Billings.Register;
using BarberBilling.Application.UseCases.Billings.Update;
using BarberBilling.Communication.Requests.Billings;
using BarberBilling.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace BarberBilling.Api.Controller;

[ApiController]
[Route("api/[controller]")]
public class BillingController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(RegisterBillingOutput), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterBilling(
        [FromServices] IRegisterBillingUseCase useCase,
        [FromBody] BillingRequestJson request)
    {
        var output = await useCase.Execute(request.ToInput());
        return Created(string.Empty, output.ToResponse());
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<GetAllBillingOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetBillings(
        [FromServices] IGetAllBillingUseCase useCase)
    {
        var output = await useCase.Execute();
        if (output is null)
            return NoContent();

        return Ok(output.ToResponse());
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(GetByIdBillingOutput), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBilling(
        [FromServices] IGetByIdBillingUseCase useCase,
        [FromRoute] Guid id)
    {
        var output = await useCase.Execute(id);
        return Ok(output.ToResponse());
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
        await useCase.Execute(id, request.ToInput());
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
}