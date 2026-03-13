
using BarberBilling.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BarberBilling.Infrastructure.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Billing> Billings { get; set; }
}