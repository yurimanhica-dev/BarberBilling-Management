using BarberBilling.Domain.Entities;
using BarberBilling.Domain.Entities.QueryParameters;
using BarberBilling.Domain.Enums;
using BarberBilling.Domain.Repositories.Billings;
using BarberBilling.Infrastructure.Context;
using Humanizer;
using Microsoft.EntityFrameworkCore;

namespace BarberBilling.Infrastructure.Persistence.Repositories;

public class BillingRepository : IBillingWriteOnlyRepository, IBillingReadOnlyRepository, IBillingUpdateOnlyRepository
{
    private readonly ApplicationDbContext _dbContext;
    public BillingRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Add(Billing billing)
    {
        await _dbContext.Billings.AddAsync(billing);
    }

    public async Task<bool> Delete(Guid Id)
    {
        var result = await _dbContext.Billings.FirstOrDefaultAsync(x => x.Id == Id);

        if (result is null)
            return false;

        _dbContext.Billings.Remove(result);
        return true;
    }

    public async Task<(List<Billing> Items, int TotalCount)> GetAll(BillingFilter filter, Guid userId, string role)
    {
        var query = _dbContext.Billings.AsQueryable();

        // Admin vê tudo, Owner e Barber veem só os seus
        if (role == "Barber")
            query = query.Where(b => b.BarberIdentifier == userId);

        if (filter.Status.HasValue)
            query = query.Where(b => b.Status == filter.Status.Value);

        query = filter.SortBy switch
        {
            "amount" => filter.Order == "asc"
                ? query.OrderBy(b => b.Amount)
                : query.OrderByDescending(b => b.Amount),

            _ => filter.Order == "asc"
                ? query.OrderBy(b => b.CreatedAt)
                : query.OrderByDescending(b => b.CreatedAt)
        };

        int totalCount = await query.CountAsync();

        var items = await query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    async Task<Billing?> IBillingReadOnlyRepository.GetById(Guid id)
    {
        return await _dbContext.Billings.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }
    async Task<Billing?> IBillingUpdateOnlyRepository.GetById(Guid id)
    {
        return await _dbContext.Billings.FirstOrDefaultAsync(x => x.Id == id);
    }
    public void Update(Billing billing)
    {
        _dbContext.Billings.Update(billing);
    }

    public Task<List<Billing>> GetByRange(DateOnly start, DateOnly end, Status? status = null)
    {
        var from = DateTime.SpecifyKind(start.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
        var to = DateTime.SpecifyKind(end.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);

        return _dbContext.Billings
            .AsNoTracking()
            .Where(x => x.Date >= from && x.Date < to)
            .Where(x => !status.HasValue || x.Status == status.Value)
            .OrderBy(x => x.Date)
            .ToListAsync();
    }
}