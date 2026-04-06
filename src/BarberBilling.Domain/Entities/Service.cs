using BarberBilling.Domain.Enums;

namespace BarberBilling.Domain.Entities;

public class Service
{
    public Guid Id { get; set; }
    public Category Category { get; set; }
    public Services Services { get; set; }
    public decimal Price { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime DeletedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}