
using BarberBilling.Domain.Entities;
using BarberBilling.Domain.Entities.Login;
using Microsoft.EntityFrameworkCore;

namespace BarberBilling.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Billing> Billings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
}