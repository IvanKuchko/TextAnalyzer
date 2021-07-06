using System;
using System.Security.Cryptography;
using System.Text;

namespace TextAnalyzer.Server.Helpers
{
    public static class PasswordHash
    {
        public static string Sha256(string value) => 
            BitConverter.ToString(
                SHA256.Create()
                .ComputeHash(
                    Encoding.UTF8.GetBytes(value)))
                .Replace("-", "");
    }
}
