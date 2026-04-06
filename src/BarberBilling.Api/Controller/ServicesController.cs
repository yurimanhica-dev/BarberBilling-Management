using BarberBilling.Api.Security.Authorization;
using BarberBilling.Application.Mappings;
using BarberBilling.Application.UseCases.Services.Delete;
using BarberBilling.Application.UseCases.Services.GetAll;
using BarberBilling.Application.UseCases.Services.Register;
using BarberBilling.Communication.Requests.Services;
using BarberBilling.Communication.Requests.Services.GetAllFilter;
using BarberBilling.Communication.Responses.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberBilling.Api.Controller;

[Authorize]
[ApiController]
[Route("api/v1/services")]
public class ServicesController : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = Permissions.Services.Create)]
    [ProducesResponseType(typeof(ResponseServiceJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromBody] RequestServiceJson request,
        [FromServices] IRegisterServiceUseCase useCase)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpGet]
    [Authorize(Policy = Permissions.Services.Read)]
    [ProducesResponseType(typeof(ResponseServicesJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll(
        [FromServices] IGetAllServicesUseCase useCase,
        [FromQuery] ServiceFilterQuery filter)
    {
        var response = await useCase.Execute(filter.ToFilter());
        return Ok(response);
    }

    // [HttpPut("{id}")]
    // [Authorize(Policy = Permissions.Services.Update)]
    // [ProducesResponseType(typeof(ResponseServiceJson), StatusCodes.Status200OK)]
    // public async Task<IActionResult> Update(
    //     [FromRoute] Guid id,
    //     [FromBody] RequestServiceJson request,
    //     [FromServices] IUpdateServiceUseCase useCase)
    // {
    //     var response = await useCase.Execute(id, request);
    //     return Ok(response);
    // }

    [HttpDelete("{id}/soft-delete")]
    [Authorize(Policy = Permissions.Services.Delete)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SoftDelete(
        [FromRoute] Guid id,
        [FromBody] bool deleted,
        [FromServices] IDeleteServiceUseCase useCase)
    {
        var response = await useCase.Execute(id, deleted);
        return Ok(response);
    }
}