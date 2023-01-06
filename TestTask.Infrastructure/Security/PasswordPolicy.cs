using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Infrastructure.Security
{
    internal class PasswordPolicy
    {
        internal static string HashPassword(string password)
        {
            const string pepper = "test-task-pepper";

            var combinedPassword = string.Concat(password, pepper);

            var sha256 = new SHA256Managed();
            var bytes = Encoding.UTF8.GetBytes(combinedPassword);
            var hash = sha256.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        internal static bool ValidatePassword(string password, string hash) =>
            HashPassword(password) == hash;
    }

}
