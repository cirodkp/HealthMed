using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using HealthMed.Application.Interfaces;

namespace HealthMed.Application.Services
{
    public static class PasswordService
    {
        public static string CalculaPasswordHash_Sha512(string rawData, string saltData)
        {
            using (var sha512 = SHA512.Create())
            {
                // Concatena a senha com o salt
                var saltedData = rawData + saltData;

                var bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(saltedData));
                var builder = new StringBuilder();

                foreach (var b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }

                return builder.ToString();
            }
        }
    }
}
