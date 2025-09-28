using System.Security.Cryptography;

namespace FitAPI.Services;


public class PasswordService : IPasswordService
{
    
    private static readonly int _iterations = 100; // Should be a lot higher in production
    private static readonly int _hashLength = 32;
    
    private static byte[] _GenerateSalt()
    {
        byte[] salt = new byte[16];
        RandomNumberGenerator.Fill(salt);
        return salt;
    }
    
    public String HashPassword(String password)
    {
        byte[] salt = _GenerateSalt();
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _iterations, HashAlgorithmName.SHA256);
        byte[] hash = pbkdf2.GetBytes(_hashLength);
        return $"{_iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(hash)}";
    }
    
    public bool VerifyPassword(string password, string storedHash)
    {
        var parts = storedHash.Split('.');
        int iterations = int.Parse(parts[0]);
        byte[] salt = Convert.FromBase64String(parts[1]);
        byte[] storedHashBytes = Convert.FromBase64String(parts[2]);
        
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        byte[] computedHash = pbkdf2.GetBytes(storedHashBytes.Length);

        return CryptographicOperations.FixedTimeEquals(computedHash, storedHashBytes);

    }

}