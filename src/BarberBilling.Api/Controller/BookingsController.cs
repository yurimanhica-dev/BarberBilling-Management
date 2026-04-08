using System.Security.Claims;
using BarberBilling.Api.Security.Authorization;
using BarberBilling.Application.Mappings;
using BarberBilling.Application.UseCases.Bookings.Delete;
using BarberBilling.Application.UseCases.Bookings.GetAll;
using BarberBilling.Application.UseCases.Bookings.GetById;
using BarberBilling.Application.UseCases.Bookings.Register;
using BarberBilling.Application.UseCases.Bookings.Update;
using BarberBilling.Communication.Requests.Bookings;
using BarberBilling.Communication.Requests.Bookings.GetAllFilter;
using BarberBilling.Communication.Responses;
using BarberBilling.Communication.Responses.Bookings.GetById;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberBilling.Api.Controller;

[ApiController]
[Route("api/v1/bookings")]
public class BookingsController : ControllerBase
{
    [HttpPost]
    [Authorize(Policy = Permissions.Bookings.Create)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(BookingRequestJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> RegisterBooking(
        [FromServices] IRegisterBookingUseCase useCase,
        [FromBody] BookingRequestJson request)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.Sid)!.Value);
        var output = await useCase.Execute(request, userId);
        return Created(string.Empty, output);
    }

    [HttpGet]
    [Authorize(Policy = Permissions.Bookings.Read)]
    [ProducesResponseType(typeof(List<ResponseBookingJson>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetBookings(
        [FromServices] IGetAllBookingUseCase useCase,
        [FromQuery] BookingFilterQuery query)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.Sid)!.Value);
        var role = User.FindFirst(ClaimTypes.Role)?.Value!;

        var response = await useCase.Execute(query.ToFilter(), userId, role);

        if (response is null || !response.Bookings.Any())
            return NoContent();

        return Ok(response);
    }

    [HttpGet]
    [Authorize(Policy = Permissions.Bookings.ReadById)]
    [Route("{id}")]
    [ProducesResponseType(typeof(ResponseBookingJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBooking(
        [FromServices] IGetByIdBookingUseCase useCase,
        [FromRoute] Guid id)
    {
        var response = await useCase.Execute(id);
        return Ok(response);
    }

    [HttpPut]
    [Authorize(Policy = Permissions.Bookings.Update)]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBooking(
        [FromServices] IUpdateBookingUseCase useCase,
        [FromRoute] Guid id,
        [FromBody] BookingUpdateRequestJson request)
    {
        await useCase.Execute(id, request);
        return NoContent();
    }

    [HttpDelete]
    [Authorize(Policy = Permissions.Bookings.Delete)]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBooking(
        [FromServices] IDeleteBookingUseCase useCase,
        [FromRoute] Guid id)
    {
        await useCase.Execute(id);
        return NoContent();
    }
}
