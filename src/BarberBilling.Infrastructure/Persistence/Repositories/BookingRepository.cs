using BarberBilling.Domain.Entities.Bookings;
using BarberBilling.Domain.Entities.Filters;
using BarberBilling.Domain.Repositories.Bookings;
using BarberBilling.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BarberBilling.Infrastructure.Persistence.Repositories;

public class BookingRepository : IBookingWriteOnlyRepository, IBookingReadOnlyRepository, IBookingUpdateOnlyRepository
{
    private readonly ApplicationDbContext _dbContext;

    public BookingRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Booking booking)
    {
        await _dbContext.Bookings.AddAsync(booking);
    }

    public async Task Update(Booking booking)
    {
        _dbContext.Bookings.Update(booking);
    }

    public async Task<bool> Delete(Guid id)
    {
        var result = await _dbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);

        if (result is null)
            return false;

        _dbContext.Bookings.Remove(result);
        return true;
    }

    public async Task<(List<Booking> Items, int TotalCount)> GetAll(BookingFilter filter, Guid userId, string role)
    {
        var query = _dbContext.Bookings
            .Include(b => b.Services)
            .AsQueryable();

        // Admin vê tudo, Client vê só os seus, Barber vê os agendados com ele
        if (role == "Client")
            query = query.Where(b => b.ClientIdentifier == userId);
        else if (role == "Barber")
            query = query.Where(b => b.BarberIdentifier == userId);

        if (filter.Status.HasValue)
            query = query.Where(b => b.Status == filter.Status.Value);

        if (filter.StartDate.HasValue)
            query = query.Where(b => b.ScheduledDate >= filter.StartDate.Value.ToDateTime(TimeOnly.MinValue));

        if (filter.EndDate.HasValue)
            query = query.Where(b => b.ScheduledDate <= filter.EndDate.Value.ToDateTime(TimeOnly.MaxValue));

        query = filter.SortBy switch
        {
            "scheduledDate" => filter.Order == "asc"
                ? query.OrderBy(b => b.ScheduledDate)
                : query.OrderByDescending(b => b.ScheduledDate),

            _ => filter.Order == "asc"
                ? query.OrderBy(b => b.CreatedAt)
                : query.OrderByDescending(b => b.CreatedAt)
        };

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    public async Task<Booking?> GetById(Guid id)
    {
        return await _dbContext.Bookings
            .Include(b => b.Services)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<List<Booking>> GetByClient(Guid clientId)
    {
        return await _dbContext.Bookings
            .Include(b => b.Services)
            .Where(b => b.ClientIdentifier == clientId)
            .OrderByDescending(b => b.ScheduledDate)
            .ToListAsync();
    }

    public async Task<List<Booking>> GetByBarber(Guid barberId)
    {
        return await _dbContext.Bookings
            .Include(b => b.Services)
            .Where(b => b.BarberIdentifier == barberId)
            .OrderByDescending(b => b.ScheduledDate)
            .ToListAsync();
    }

    public async Task<List<Booking>> GetByDateRange(DateOnly start, DateOnly end)
    {
        return await _dbContext.Bookings
            .Include(b => b.Services)
            .Where(b => b.ScheduledDate >= start.ToDateTime(TimeOnly.MinValue) &&
                b.ScheduledDate <= end.ToDateTime(TimeOnly.MaxValue))
            .OrderBy(b => b.ScheduledDate)
            .ToListAsync();
    }
}
