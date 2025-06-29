using System;
using System.Security.Cryptography;

namespace InstantCodeLab.Infrastructure.Utilities;

public static class PasswordHasher
{
    public static string HashPassword(string password, string salt)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 100_000, HashAlgorithmName.SHA256);
        byte[] hashBytes = pbkdf2.GetBytes(32);
        return Convert.ToBase64String(hashBytes);
    }

    public static bool VerifyPassword(string password, string storedHash, string salt)
    {
        string computedHash = HashPassword(password, salt);
        return computedHash == storedHash;
    }
}
