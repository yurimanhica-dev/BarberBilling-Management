using BarberBilling.Domain.Entities.Bookings;
using BarberBilling.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberBilling.Infrastructure.Persistence.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("Bookings");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(b => b.ClientIdentifier)
            .IsRequired();

        builder.Property(b => b.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(b => b.BarberIdentifier)
            .IsRequired();

        builder.Property(b => b.ScheduledDate)
            .IsRequired();

        builder.Property(b => b.Status)
            .HasDefaultValue(BookingStatus.Pending)
            .HasConversion<int>();

        builder.Property(b => b.Notes)
            .HasMaxLength(500);

        builder.Property(b => b.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.Property(b => b.UpdatedAt)
            .IsRequired()
            .HasDefaultValueSql("now()");

        builder.HasMany(b => b.Services)
            .WithOne()
            .HasForeignKey(bs => bs.BookingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
