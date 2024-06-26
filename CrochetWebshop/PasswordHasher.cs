using System;
using System.Security.Cryptography;
using System.Text;

namespace CrochetWebshop
{
    public class PasswordHasher
    {
        private const int Iterations = 10000;

        private const int KeySize = 32;

        // Instellingen voor het hashen
        private const int SaltSize = 16; // 128 bit

        // 256 bit
        // Aantal iteraties

        public static string HashPassword(string password)
        {
            // Maak een cryptografisch veilige salt
            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[SaltSize];
                rng.GetBytes(salt);

                // Hash het wachtwoord met de salt
                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
                {
                    byte[] hash = pbkdf2.GetBytes(KeySize);

                    // Combineer de salt en het hash en converteer naar een string
                    byte[] hashBytes = new byte[SaltSize + KeySize];
                    Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                    Array.Copy(hash, 0, hashBytes, SaltSize, KeySize);

                    return Convert.ToBase64String(hashBytes);
                }
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // Haal de bytes op van de opgeslagen hash
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            // De eerste SaltSize bytes zijn de salt
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // De rest is de hash
            byte[] storedHash = new byte[KeySize];
            Array.Copy(hashBytes, SaltSize, storedHash, 0, KeySize);

            // Hash het gegeven wachtwoord met dezelfde salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations))
            {
                byte[] hash = pbkdf2.GetBytes(KeySize);

                // Vergelijk de hash van het gegeven wachtwoord met de opgeslagen hash
                for (int i = 0; i < KeySize; i++)
                {
                    if (hash[i] != storedHash[i])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}