using System.Security.Claims;
using BarberBilling.Application.UseCases.Authentication.Login;
using BarberBilling.Application.UseCases.Authentication.Refresh;
using BarberBilling.Application.UseCases.Authentication.Revoke;
using BarberBilling.Application.UseCases.User.Register;
using BarberBilling.Communication.Requests.Authentication.login;
using BarberBilling.Communication.Requests.Authentication.RefreshToken;
using BarberBilling.Communication.Requests.Authentication.RegisterClient;
using BarberBilling.Communication.Responses;
using BarberBilling.Communication.Responses.Authentication;
using BarberBilling.Communication.Responses.Authentication.RegisterClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberBilling.Api.Controller;

[Route("api/v1/authentication")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    [HttpPost("register-cliente")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResponseRegisterClientJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Register(
        [FromBody] RequestRegisterClientJson request,
        [FromServices] IRegisterClientUseCase useCase)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResponseTokensJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login(
    [FromBody] RequestLoginJson request,
    [FromServices] ILoginUserUseCase useCase)
    {
        var response = await useCase.Execute(request);
        return Ok(response);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ResponseTokensJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Refresh(
        [FromBody] RequestRefreshTokenJson request,
        [FromServices] IRefreshTokenUseCase useCase)
    {
        var response = await useCase.Execute(request.RefreshToken);
        return Ok(response);
    }

    [HttpDelete("logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Logout(
        [FromServices] IRevokeTokenUseCase useCase)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.Sid)!.Value);
        await useCase.Execute(userId);
        return NoContent();
    }
}