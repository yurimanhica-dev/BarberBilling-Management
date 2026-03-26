using BarberBilling.Application.UseCases.User.Register;
using BarberBilling.Communication.Requests.Users;
using BarberBilling.Communication.Responses;
using BarberBilling.Communication.Responses.User.Register;
using Microsoft.AspNetCore.Mvc;

namespace BarberBilling.Api.Controller;

[Route("api/v1/users")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromServices] IRegisterUserUseCase useCase,
        [FromBody] RequestRegisterUserJson request)
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}