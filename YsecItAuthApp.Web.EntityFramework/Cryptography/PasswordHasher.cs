using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace YsecItAuthApp.Web.EntityFramework.Cryptography
{

    public static class PasswordHasher
    {
        /// <summary>
        /// Creates a <paramref name="passwordHash"/> representation of the supplied <paramref name="password"/> and also gives out the <paramref name="passwordSalt"/> for eventual storage
        /// </summary>
        /// <param name="password"></param>
        /// <param name="passwordHash"></param>
        /// <param name="passwordSalt"></param>
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (String.IsNullOrWhiteSpace(password)) { throw new ArgumentException($" {nameof(password)}  cannot be empty or whitespace"); }

            using var hmac = new HMACSHA512();

            passwordSalt = hmac.Key;

            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        /// <summary>
        /// Verifies a given <paramref name="password"/> against it's <paramref name="storedHash"/> and <paramref name="storedSalt"/> values.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="storedHash"></param>
        /// <param name="storedSalt"></param>
        /// <returns><c>True</c>: When successful verification occurs; <c>False</c> for wrong passwords</returns>
        /// <exception cref="ArgumentException"></exception>
        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (String.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException(nameof(password) + " cannot be empty or whitespace");
            }

            if (storedHash.Length != 64)
            {
                throw new ArgumentException("Invalid length of password hash(64 bytes expected).", nameof(storedHash));
            }

            if (storedSalt.Length != 128)
            {
                throw new ArgumentException("Invalid length of password salt (128 bytes expected).", nameof(storedSalt));
            }

            using var hmac = new HMACSHA512(storedSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return !computedHash.Where((t, i) => t != storedHash[i]).Any();
        }

    }
}
