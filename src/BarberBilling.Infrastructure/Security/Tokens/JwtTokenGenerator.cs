using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BarberBilling.Domain.Entities;
using BarberBilling.Domain.Entities.Login;
using BarberBilling.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace BarberBilling.Infrastructure.Security.Tokens;

public class JwtTokenGenerator : IAccessTokenGenerator, IRefreshTokenGenerator
{
    private readonly uint _expirationInMinutes;
    private readonly uint _expirationInDays;
    private readonly string _secretKey;

    public JwtTokenGenerator(uint expirationInMinutes, string secretKey, uint expirationInDays)
    {
        _expirationInMinutes = expirationInMinutes;
        _secretKey = secretKey;
        _expirationInDays = expirationInDays;
    }
    public string Generate(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Sid, user.UserIdentifier.ToString()),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("token_version", user.TokenVersion.ToString()),

        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims), 
            Expires = DateTime.UtcNow.AddMinutes(_expirationInMinutes),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    public RefreshToken GenerateRefreshToken(Guid userId)
    {
        var randomBytes = new byte[64];
        RandomNumberGenerator.Fill(randomBytes);
        
        return new RefreshToken
        {
            Token = Convert.ToBase64String(randomBytes),
            UserId = userId,
            ExpiresAt = DateTime.UtcNow.AddDays(_expirationInDays)
        };
    }
}