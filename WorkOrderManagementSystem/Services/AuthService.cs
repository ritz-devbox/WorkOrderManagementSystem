using System.Security.Cryptography;
using System.Text;

namespace WorkOrderManagementSystem.Services;

public class AuthService
{
    public string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    public bool VerifyPassword(string password, string hash)
    {
        var hashOfInput = HashPassword(password);
        return hashOfInput == hash;
    }

    public string GenerateSessionToken()
    {
        return Guid.NewGuid().ToString("N");
    }
}
