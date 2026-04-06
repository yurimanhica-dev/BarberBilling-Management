using BarberBilling.Communication.Responses.Authorization;
using BarberBilling.Domain.Entities.Authorization;

namespace BarberBilling.Application.Mappings;

public static class AuthorizationMapping
{
    public static ResponseRoleJson ToResponseRegisterRole(this Role role)
    {
        return new ResponseRoleJson
        {
            RoleIdentifier = role.RoleIdentifier,
            Name = role.Name
        };   
    }

    public static ResponseRoleJson ToResponseGetRole(this Role role)
    {
        return new ResponseRoleJson
        {
            RoleIdentifier = role.RoleIdentifier,
            Name = role.Name,
            Permissions = role.RolePermissions
                .Select(rp => rp.Permission.Name)
                .ToList()
        };   
    }

    public static ResponsePermissionJson ToResponse(this Permission permission)
    {
        return new ResponsePermissionJson
        {
            PermissionIdentifier = permission.PermissionIdentifier,
            Name = permission.Name
        };
    }
}