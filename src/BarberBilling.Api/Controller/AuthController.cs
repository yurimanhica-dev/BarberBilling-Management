using System.Security.Claims;
using BarberBilling.Application.UseCases.User.Login;
using BarberBilling.Application.UseCases.User.Refresh;
using BarberBilling.Application.UseCases.User.Revoke;
using BarberBilling.Communication.Requests.Auth;
using BarberBilling.Communication.Requests.Login;
using BarberBilling.Communication.Responses;
using BarberBilling.Communication.Responses.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberBilling.Api.Controller;

[Route("api/v1/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
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
    [Authorize]
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