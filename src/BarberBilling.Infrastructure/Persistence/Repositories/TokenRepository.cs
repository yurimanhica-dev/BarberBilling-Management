using BarberBilling.Domain.Entities.Login;
using BarberBilling.Domain.Repositories.Token;
using BarberBilling.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BarberBilling.Infrastructure.Persistence.Repositories;

public class TokenRepository : ITokenReadOnlyRepository, ITokenWriteOnlyRepository
{
    private readonly ApplicationDbContext _dbContext;

    public TokenRepository(ApplicationDbContext context)
    {
        _dbContext = context;
    }
    public async Task<RefreshToken?> GetByValue(string value)
    {
        return await _dbContext.RefreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(rt => rt.Token == value);
    }

    public async Task SaveRefreshToken(RefreshToken refreshToken)
    {
        await _dbContext.RefreshTokens.AddAsync(refreshToken);
    }
    
    public async Task DeleteAllByUserId(Guid userId)
    {
        var tokens = _dbContext.RefreshTokens.Where(rt => rt.UserId == userId);
        _dbContext.RefreshTokens.RemoveRange(tokens);
    }
}