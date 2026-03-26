using System.Security.Cryptography;
using BarberBilling.Domain.Security.Cryptography;
using Isopoh.Cryptography.Argon2;

namespace BarberBilling.Infrastructure.Security.Cryptography;

public class Argon2id : IPasswordEncripte
{
    private readonly string _pepper;
    public Argon2id(string pepper) => _pepper = pepper;
    private const string Separator = "::";
    public string Encrypt(string password)
    {
        var passwordWithPepper = $"{password}{Separator}{_pepper}";

        var salt = new byte[16];
        RandomNumberGenerator.Fill(salt);
        
        var config = new Argon2Config
        {
            Type = Argon2Type.HybridAddressing,
            Version = Argon2Version.Nineteen,
            TimeCost = 4,
            MemoryCost = 65536,
            Lanes = 2,
            Threads = 2,
            Password = System.Text.Encoding.UTF8.GetBytes(passwordWithPepper),
            Salt = salt,
            HashLength = 32
        };

        return Argon2.Hash(config);
    }

    public bool Verify(string password, string hash)
    {
        var passwordWithPepper = $"{password}{Separator}{_pepper}";

        return Argon2.Verify(hash, System.Text.Encoding.UTF8.GetBytes(passwordWithPepper));
    }
}