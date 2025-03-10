using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using Flunt.Br;

namespace Domain.ValueObjects;

public sealed class Password : BaseValueObject
{
    public string Hash { get; private set; }
    public string Salt { get; private set; }
    [NotMapped]
    public string Content { get; private set; }

    public Password() { }

    public Password(string? password, bool forAuthentication = false)
    {
        if (forAuthentication)
        {
            Content = password; 
            return;
        }
        
        AddNotifications(
            new Contract()
                .Requires()
                .IsNotNullOrEmpty(password, nameof(Password), "Password cannot be null or empty")
                .IsTrue(password != null && password.Length > 6, nameof(Password), "Password must be at least 6 characters long")
        );

        if (!IsValid) return;

        Salt = GenerateSalt();
        Hash = GenerateHash(password, Salt);
    }
    
    private string GenerateSalt()
    {
        var saltBytes = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(saltBytes);
        }
        return Convert.ToBase64String(saltBytes);
    }

    private string GenerateHash(string password, string salt)
    {
        using var sha256 = SHA256.Create();
        var combinedBytes = Encoding.UTF8.GetBytes(password + salt);
        var hashBytes = sha256.ComputeHash(combinedBytes);
        return Convert.ToBase64String(hashBytes);
    }


    public bool VerifyPassword(string password, string storedSalt)
    {
        var hashToVerify = GenerateHash(password, storedSalt); 
        return hashToVerify == Hash; 
    }
}