using System.Security.Claims;
using BarberBilling.Application.Mappings;
using BarberBilling.Application.UseCases.Billings.Delete;
using BarberBilling.Application.UseCases.Billings.GetAll;
using BarberBilling.Application.UseCases.Billings.GetById;
using BarberBilling.Application.UseCases.Billings.Register;
using BarberBilling.Application.UseCases.Billings.Update;
using BarberBilling.Communication.Requests.Billings;
using BarberBilling.Communication.Requests.Billings.GetAllFilter;
using BarberBilling.Communication.Responses;
using BarberBilling.Communication.Responses.Billings.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberBilling.Api.Controller;

[ApiController]
[Route("api/v1/billings")]
public class BillingController : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Admin, Barber, Manager")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BillingRequestJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterBilling(
        [FromServices] IRegisterBillingUseCase useCase,
        [FromBody] BillingRequestJson request)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.Sid)!.Value);
        var output = await useCase.Execute(request, userId);
        return Created(string.Empty, output);
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Barber, Manager")]
    [ProducesResponseType(typeof(List<ResponseBillingJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetBillings(
        [FromServices] IGetAllBillingUseCase useCase,
        [FromQuery] BillingFilterQuery query)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.Sid)!.Value);
        var role = User.FindFirst(ClaimTypes.Role)?.Value!;

        var response = await useCase.Execute(query.ToFilter(), userId, role);

        if (response is null || !response.Billings.Any())
            return NoContent();

        return Ok(response);
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Manager")]
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
    [Authorize(Roles = "Admin, Barber, Manager")]
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
    [Authorize(Roles = "Admin, Manager")]
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