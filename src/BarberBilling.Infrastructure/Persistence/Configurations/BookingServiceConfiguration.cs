using BarberBilling.Domain.Entities.Bookings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BarberBilling.Infrastructure.Persistence.Configurations;

public class BookingServiceConfiguration : IEntityTypeConfiguration<BookingService>
{
    public void Configure(EntityTypeBuilder<BookingService> builder)
    {
        builder.ToTable("BookingServices");

        builder.HasKey(bs => bs.Id);

        builder.Property(bs => bs.Id)
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(bs => bs.BookingId)
            .IsRequired();

        builder.Property(bs => bs.ServiceIdentifier)
            .IsRequired();

        builder.Property(bs => bs.ServiceType)
            .IsRequired();

        builder.Property(bs => bs.Price)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        
        builder.HasOne<Booking>()
            .WithMany(b => b.Services)
            .HasForeignKey(bs => bs.BookingId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
