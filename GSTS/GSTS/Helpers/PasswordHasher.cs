using System.Security.Cryptography;

namespace GSTS.Helpers;

// Security: officer passwords are stored hashed, never as plain text.
// Uses PBKDF2 (built into .NET) with SHA-256, a random 16-byte salt
// and 100,000 iterations. Stored format: "base64salt:base64hash".
public static class PasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100_000;

    public static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);
        return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
    }

    public static bool VerifyHash(string password, string storedHash)
    {
        string[] parts = storedHash.Split(':');
        if (parts.Length != 2)
        {
            return false;
        }

        byte[] salt = Convert.FromBase64String(parts[0]);
        byte[] expected = Convert.FromBase64String(parts[1]);
        byte[] actual = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, expected.Length);

        // Compare in constant time so the comparison does not leak timing information.
        return CryptographicOperations.FixedTimeEquals(actual, expected);
    }
}
