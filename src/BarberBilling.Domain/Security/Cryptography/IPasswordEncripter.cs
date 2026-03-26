namespace BarberBilling.Domain.Security.Cryptography;

public interface IPasswordEncripte
{
    string Encrypt(string password);
    bool Verify(string password, string hash);
}
