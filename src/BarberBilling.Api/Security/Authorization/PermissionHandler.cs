using System.Security.Claims;
using BarberBilling.Domain.Repositories.User;
using Microsoft.AspNetCore.Authorization;

namespace BarberBilling.Api.Security.Authorization;

public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly IUserReadOnlyRepository _userRepository;

    public PermissionHandler(IUserReadOnlyRepository userRepository)
    {
        _userRepository = userRepository;
    }

    protected override async Task HandleRequirementAsync(
    AuthorizationHandlerContext context,
    PermissionRequirement requirement)
    {
        var userId = context.User.FindFirst(ClaimTypes.Sid)?.Value;

        if (string.IsNullOrWhiteSpace(userId))
        {
            context.Fail();
            return;
        }

        var user = await _userRepository
            .GetByIdentifierWithPermissions(Guid.Parse(userId));

        if (user is null)
        {
            context.Fail();
            return;
        }

        // 🔥 proteção aqui
        var permissions = user.Role?.RolePermissions;

        if (permissions is null || !permissions.Any())
        {
            context.Fail();
            return;
        }

        var hasPermission = permissions
            .Any(rp => rp.Permission != null &&
                    rp.Permission.Name == requirement.Permission);

        if (hasPermission)
            context.Succeed(requirement);
        else
            context.Fail();
    }
}