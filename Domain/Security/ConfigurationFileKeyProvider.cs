using Microsoft.Extensions.Configuration;
using System;

namespace Domain.Security
{
    public class ConfigurationFileKeyProvider : IEncryptionKeyProvider
    {
        private readonly IConfiguration _config;

        public ConfigurationFileKeyProvider(IConfiguration config)
        {
            _config = config;
        }

        public string PublicKey => _config["RSAKeys:Public"];

        public string PrivateKey => _config["RSAKeys:Private"];
    }
}
