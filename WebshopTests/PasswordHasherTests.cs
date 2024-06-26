using NUnit.Framework;
using CrochetWebshop;

namespace CrochetWebshop.Tests
{
    [TestFixture]
    public class PasswordHasherTests
    {
        [Test]
        public void HashPassword_ShouldReturnDifferentHashesForSamePassword()
        {
            // Arrange
            string password = "MySecurePassword";

            // Act
            string hash1 = PasswordHasher.HashPassword(password);
            string hash2 = PasswordHasher.HashPassword(password);

            // Assert
            Assert.AreNotEqual(hash1, hash2);
        }

        [Test]
        public void VerifyPassword_ShouldReturnFalseForInvalidPassword()
        {
            // Arrange
            string password = "MySecurePassword";
            string hashedPassword = PasswordHasher.HashPassword(password);
            string invalidPassword = "InvalidPassword";

            // Act
            bool result = PasswordHasher.VerifyPassword(invalidPassword, hashedPassword);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void VerifyPassword_ShouldReturnFalseForModifiedHash()
        {
            // Arrange
            string password = "MySecurePassword";
            string hashedPassword = PasswordHasher.HashPassword(password);
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);

            // Modify the hash slightly
            hashBytes[hashBytes.Length - 1]++;
            string modifiedHash = Convert.ToBase64String(hashBytes);

            // Act
            bool result = PasswordHasher.VerifyPassword(password, modifiedHash);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void VerifyPassword_ShouldReturnTrueForValidPassword()
        {
            // Arrange
            string password = "MySecurePassword";
            string hashedPassword = PasswordHasher.HashPassword(password);

            // Act
            bool result = PasswordHasher.VerifyPassword(password, hashedPassword);

            // Assert
            Assert.IsTrue(result);
        }
    }
}