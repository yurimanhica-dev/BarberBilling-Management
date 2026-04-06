using BarberBilling.Api.Security.Authorization;
using BarberBilling.Application.UseCases.User;
using BarberBilling.Communication.Requests.Users;
using BarberBilling.Communication.Responses;
using BarberBilling.Communication.Responses.User.Register;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberBilling.Api.Controller;

[Route("api/v1/users")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost("register")]
    [Authorize(Policy = Permissions.Users.Create)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ResponseRegisterUserJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromBody] RequestRegisterUserJson request,
        [FromServices] IRegisterUserUseCase useCase
        )
    {
        var response = await useCase.Execute(request);

        return Created(string.Empty, response);
    }
}