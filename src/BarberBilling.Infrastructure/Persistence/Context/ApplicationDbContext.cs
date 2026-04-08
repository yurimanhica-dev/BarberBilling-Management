
using BarberBilling.Domain.Entities;
using BarberBilling.Domain.Entities.Authorization;
using BarberBilling.Domain.Entities.Billings;
using BarberBilling.Domain.Entities.Bookings;
using BarberBilling.Domain.Entities.Login;
using Microsoft.EntityFrameworkCore;

namespace BarberBilling.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<Billing> Billings { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<RolePermissions> RolePermissions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Permission> Permissions { get; set; }

}