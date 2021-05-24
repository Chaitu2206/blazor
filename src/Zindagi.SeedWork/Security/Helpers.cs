using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Zindagi.SeedWork.Security
{
    public static class Helpers
    {
        public static string NewRandomHash() => Guid.NewGuid().ToString().Sha256Base64().Replace("+", "x");

        public static string Sha256Base64(this string value) => Sha256Base64(Encoding.UTF8.GetBytes(value));

        public static string Sha256Base64(this byte[] bytes)
        {
            using var sha = SHA256.Create();
            var bytesHash = sha.ComputeHash(bytes);

            var result = Convert.ToBase64String(bytesHash);

            return result;
        }

        public static int GetRandomNumber()
        {
            using var rng = new RNGCryptoServiceProvider();
            var data = new byte[4];

            for (var i = 0; i < 10; i++)
                rng.GetBytes(data);
            var value = BitConverter.ToInt32(data, 0);
            return value;
        }

        public static string GetRandomString(bool verySecure = false) => verySecure ? Path.GetRandomFileName().Replace(".", string.Empty) : Guid.NewGuid().ToString("N");

        public static string GetUniqueKey(int size)
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            var data = new byte[size];
            using (var crypto = new RNGCryptoServiceProvider())
                crypto.GetBytes(data);

            var result = new StringBuilder(size);
            foreach (var b in data)
                result.Append(chars[b % chars.Length]);

            return result.ToString();
        }
    }
}
