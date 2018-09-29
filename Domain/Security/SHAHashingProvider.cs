namespace Domain.Security
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    public class SHA256HashingProvider : IHashProvider
    {
        private const int saltLength = 32;

        private byte[] Combine(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, ret, 0, first.Length);
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length);

            return ret;
        }

        public byte[] GenerateHash(string text, byte[] salt)
        {
            var bytesToHash = Encoding.UTF8.GetBytes(text);

            using (var sha256 = SHA256.Create())
                return sha256.ComputeHash(Combine(bytesToHash, salt));
        }

        public byte[] GenerateSalt()
        {
            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[saltLength];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }
    }

}
