using BarberBilling.Domain.Entities.Authorization;
using BarberBilling.Domain.Repositories.User.Authorization;
using BarberBilling.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BarberBilling.Infrastructure.Persistence.Repositories;

public class AuthorizationRepository : IAuthorizationReadOnlyRepository, IAuthorizationWriteOnlyRepository
{
    private readonly ApplicationDbContext _dbContext;

    public AuthorizationRepository(ApplicationDbContext dbContext)
    { 
        _dbContext = dbContext;
    }
    public async Task AddRole(Role role)
    {
        await _dbContext.Roles.AddAsync(role);
    }
    public async Task AddPermission(Permission permission)
    {
        await _dbContext.Permissions.AddAsync(permission);
    }
    public async Task AssignPermission(RolePermissions rolePermission)
    {
        await _dbContext.RolePermissions.AddAsync(rolePermission);
    }

    public async Task<List<Permission>> GetAllPermissions()
    {
        return await _dbContext.Permissions
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .ToListAsync();
    }

    public async Task<List<Role>> GetAllRoles()
    {
        return await _dbContext.Roles
            .AsNoTracking()
            .Include(r => r.RolePermissions)
            .ThenInclude(rp => rp.Permission)
            .ToListAsync();
    }

    public async Task<RolePermissions?> GetRolePermission(Guid roleIdentifier, Guid permissionIdentifier)
    {
        return await _dbContext.RolePermissions
            .FirstOrDefaultAsync(rp =>
                rp.RoleIdentifier == roleIdentifier &&
                rp.PermissionIdentifier == permissionIdentifier);
    }

    public async Task RevokePermission(RolePermissions rolePermission)
    {
        _dbContext.RolePermissions.Remove(rolePermission);
    }

    public async Task<Role?> GetRoleByIdentifier(Guid roleIdentifier)
    {
        return await _dbContext.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.RoleIdentifier == roleIdentifier);
    } 

    public async Task<Permission?> GetPermissionByName(string name)
    {
        return await _dbContext.Permissions
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Name == name.ToLower());
    }

    public async Task<Role?> GetRoleByName(string name)
    {
        return await _dbContext.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Name == name.ToLower());
    }
}