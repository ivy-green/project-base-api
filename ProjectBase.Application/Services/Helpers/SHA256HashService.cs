using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace ProjectBase.Application.Services.Helpers
{
    [ExcludeFromCodeCoverage]
    public class SHA256HashService : IHashService
    {
        public (string, string) CreatePasswordHashAndSalt(string password)
        {
            // Hasher
            SHA256 sha256haser = SHA256.Create();

            // Preparing salt bytes
            byte[] passwordSaltBytes = RandomNumberGenerator.GetBytes(SHA256.HashSizeInBytes);

            // Compute hash for password salt byte array
            byte[] hashedSalt = sha256haser.ComputeHash(passwordSaltBytes);

            // Convert hashed salt to hex string array
            IEnumerable<string> hexStringArrayFromHashedSalt = hashedSalt.Select(h => h.ToString("x2"));

            // Convert string arrays to a single string
            string hexStringFromHashedSalt = string.Join("", hexStringArrayFromHashedSalt);

            // Append password to hexed salt
            string saltedPassword = hexStringFromHashedSalt + password;

            // Preparing salted password bytes
            byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);

            // Compute hash for salted password
            byte[] hashedSaltedPassword = sha256haser.ComputeHash(saltedPasswordBytes);

            // Convert hashed salted password to hex string array
            IEnumerable<string> hexStringArrayFromHashedSaltedPassword = hashedSaltedPassword.Select(h => h.ToString("x2"));

            // Convert hashed salted password hex string array to a single string
            string hexStringFromHashedSaltedPassword = string.Join("", hexStringArrayFromHashedSaltedPassword);

            return (hexStringFromHashedSaltedPassword, hexStringFromHashedSalt);
        }

        public bool VerifyPasswordHash(string salt, string passwordFromDb, string inputPassword)
        {
            SHA256 sha256haser = SHA256.Create();

            // Append password to hexed salt
            string saltedPassword = salt + inputPassword;

            // Preparing salted password bytes
            byte[] saltedPasswordBytes = Encoding.UTF8.GetBytes(saltedPassword);

            // Compute hash for salted password
            byte[] hashedSaltedPassword = sha256haser.ComputeHash(saltedPasswordBytes);

            // Convert hashed salted password to hex string array
            IEnumerable<string> hexStringArrayFromHashedSaltedPassword = hashedSaltedPassword.Select(h => h.ToString("x2"));

            // Convert hashed salted password hex string array to a single string
            string hexStringFromHashedSaltedPassword = string.Join("", hexStringArrayFromHashedSaltedPassword);

            return passwordFromDb.Equals(hexStringFromHashedSaltedPassword);
        }
    }
}
