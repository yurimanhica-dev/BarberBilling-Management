using BarberBilling.Application.UseCases.Authorization.AssignPermission;
using BarberBilling.Application.UseCases.Authorization.GetAllPermissions;
using BarberBilling.Application.UseCases.Authorization.GetAllRoles;
using BarberBilling.Application.UseCases.Authorization.RegisterPermission;
using BarberBilling.Application.UseCases.Authorization.RegisterRole;
using BarberBilling.Application.UseCases.Authorization.RevokePermission;
using BarberBilling.Communication.Requests.Authorization;
using BarberBilling.Communication.Responses;
using BarberBilling.Communication.Responses.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberBilling.Api.Controller;

// [Authorize(Policy = Permissions.Roles.Admin)]
[ApiController]
[Route("api/v1/authorization")]
public class AuthorizationController : ControllerBase
{
    [HttpPost("roles")]
    // [Authorize(Policy = Permissions.Authorization.CreateRole)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ResponseRoleJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateRole(
        [FromBody] RequestCreateRoleJson request,
        [FromServices] IRegisterRoleUseCase useCase)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpPost("permissions")]
    // [Authorize(Policy = Permissions.Authorization.CreatePermission)]
    [ProducesResponseType(typeof(ResponsePermissionJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreatePermission(
        [FromBody] RequestRegisterPermissionJson request,
        [FromServices] IRegisterPermissionUseCase useCase)
    {
        var response = await useCase.Execute(request);
        return Created(string.Empty, response);
    }

    [HttpGet("roles")]
    // [Authorize(Policy = Permissions.Authorization.GetAllRoles)]
    [ProducesResponseType(typeof(List<ResponseRoleJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllRoles(
        [FromServices] IGetAllRolesUseCase useCase)
    {
        var response = await useCase.Execute();
        return Ok(response);
    }

    [HttpGet("permissions")]
    // [Authorize(Policy = Permissions.Authorization.GetAllPermissions)]
    [ProducesResponseType(typeof(List<ResponsePermissionJson>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPermissions(
        [FromServices] IGetAllPermissionsUseCase useCase)
    {
        var response = await useCase.Execute();
        return Ok(response);
    }

    [HttpPost("roles/{roleId}/permissions")]
    public async Task<IActionResult> AssignPermissions(
        [FromRoute] Guid roleId,
        [FromBody] RequestPermissionsJson request,
        [FromServices] IAssignPermissionUseCase useCase)
    {
        await useCase.Execute(roleId, request);
        return Ok("Permissions assigned successfully.");
    }

    [HttpDelete("roles/{roleId}/permissions")]
    // [Authorize(Policy = Permissions.Authorization.RevokePermission)]
    [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RemovePermissions(
        [FromRoute] Guid roleId,
        [FromBody] RequestPermissionsJson request,
        [FromServices] IRevokePermissionUseCase useCase)
    {
        await useCase.Execute(roleId, request);
        return Ok("Permissions revoked successfully.");
    }
}