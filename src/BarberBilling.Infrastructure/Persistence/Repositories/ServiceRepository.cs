using BarberBilling.Domain.Entities;
using BarberBilling.Domain.Entities.Filters;
using BarberBilling.Domain.Repositories.Services;
using BarberBilling.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BarberBilling.Infrastructure.Persistence.Repositories;

public class ServiceRepository : IServiceReadOnlyRepository, IServiceWriteOnlyRepository, IServiceUpdateOnlyRepository
{
    private readonly ApplicationDbContext _dbContext;

    public ServiceRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Service service)
    {
        await _dbContext.Services.AddAsync(service);
    }

    public async Task Update(Service service)
    {
        _dbContext.Services.Update(service);
    }

    public async Task<(List<Service> Items, int TotalCount)> GetAll(ServiceFilter filter)
    {
        var query = _dbContext.Services.AsQueryable();

        // 🔍 Filtro IsDeleted
        if (filter.IsDeleted)
            query = query.Where(s => s.IsDeleted == filter.IsDeleted);
        else
            query = query.Where(s => !s.IsDeleted);

        // 🔍 Filtro por categoria
        if (filter.Category.HasValue)
            query = query.Where(s => (int)s.Category == filter.Category.Value);

        // 🔽 Ordenação
        query = filter.SortBy?.ToLower() switch
        {
            "price" => filter.Order == "asc"
                ? query.OrderBy(s => s.Price)
                : query.OrderByDescending(s => s.Price),

            _ => filter.Order == "desc"
                ? query.OrderBy(s => s.Services)
                : query.OrderByDescending(s => s.Services)
        };

        // 📊 Total antes da paginação
        int totalCount = await query.CountAsync();

        // 📄 Paginação
        var items = await query
            .Skip((filter.Page - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        return (items, totalCount);
    }

    async Task<Service?> IServiceReadOnlyRepository.GetByIdentifier(Guid serviceIdentifier)
    {
        return await _dbContext.Services
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == serviceIdentifier && !s.IsDeleted);
    }

    async Task<Service?> IServiceUpdateOnlyRepository.GetByIdentifier(Guid serviceIdentifier)
    {
        return await _dbContext.Services
            .FirstOrDefaultAsync(s => s.Id == serviceIdentifier);
    }

    public async Task SoftDelete(Service service, bool isDeleted)
    {
        service.IsDeleted = isDeleted;

        if (isDeleted == true)
            service.DeletedAt = DateTime.UtcNow;

        _dbContext.Services.Update(service);
    }

    public Task<Service?> GetByName(string name)
    {
        return _dbContext.Services
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Services.ToString() == name);
    }
}     