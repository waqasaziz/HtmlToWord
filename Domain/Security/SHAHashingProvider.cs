namespace Domain.Security
{
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class HashingProvider : IHashProvider
    {
        private const int DefaultLength = 32;

        private byte[] Combine(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;
        }

        public byte[] GenerateSalt()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[DefaultLength];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }

        public byte[] GenerateSHA256Hash(string text, byte[] salt)
        {
            var bytesToHash = Encoding.UTF8.GetBytes(text);

            using (var sha256 = SHA256.Create())
                return sha256.ComputeHash(Combine(bytesToHash, salt));
        }
        
        public byte[] GeneratePBKDF2Hash(string password, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                 password: password,
                 salt: salt,
                 prf: KeyDerivationPrf.HMACSHA256,
                 iterationCount: 10000,
                 numBytesRequested: DefaultLength);
        }
    }
}
