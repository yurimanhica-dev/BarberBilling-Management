using BarberBilling.Domain.Entities;
using BarberBilling.Domain.Repositories.User;
using BarberBilling.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BarberBilling.Infrastructure.Persistence.Repositories;

internal class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async  Task<User?> GetByEmail(string email)
    {   
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email.Equals(email));
    }

    public async Task<User?> GetByIdentifier(Guid userIdentifier)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.UserIdentifier == userIdentifier);
    }

    public async Task Update(User user)
    {
        _dbContext.Users.Update(user);
    }

    public async Task<bool> VerifyIfUserExist(string email)
    {
        return await _dbContext.Users.AnyAsync(user => user.Email.Equals(email));
    }
} 