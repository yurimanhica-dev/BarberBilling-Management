using BarberBilling.Domain.Enums;

namespace BarberBilling.Domain.Repositories.Categories;

public interface ICategoryReadOnlyRepository
{
    Task<List<Category>> GetAllAsync();
}