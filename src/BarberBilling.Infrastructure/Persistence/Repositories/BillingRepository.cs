using BarberBilling.Domain.Entities;
using BarberBilling.Domain.Repositories.Billings;
using BarberBilling.Infrastructure.Context;
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

    public async Task<List<Billing>> GetAll()
    {
        return await _dbContext.Billings.ToListAsync();
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
}