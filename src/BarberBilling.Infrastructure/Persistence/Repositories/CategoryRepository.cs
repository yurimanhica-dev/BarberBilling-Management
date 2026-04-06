using BarberBilling.Domain.Enums;
using BarberBilling.Domain.Repositories.Categories;
using BarberBilling.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BarberBilling.Infrastructure.Persistence.Repositories;

public class CategoryRepository : ICategoryReadOnlyRepository
{
    private readonly ApplicationDbContext _DbContext;
    public CategoryRepository(ApplicationDbContext dbContext)
    {
        _DbContext = dbContext;
    }
    public async Task<List<Category>> GetAllAsync()
    {
        var categories = await _DbContext.Services
        .AsNoTracking()
        .Select(s => s.Category)
        .Distinct()
        .OrderBy(c => c)
        .ToListAsync();
        
        return categories;
    }
}