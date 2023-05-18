using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace BadHunter.BL.Auth
{
    public class Encrypter : IEncrypter
    {
        public string HashPassword(string password, string salt) =>
            Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                System.Text.Encoding.ASCII.GetBytes(salt),
                KeyDerivationPrf.HMACSHA512,
                5000,
                64
            ));
    }
}