namespace Domain.Data
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Security;
    using System;
    using System.Linq;

    public static class SeedData
    {
        private const string Word = "Lorem Ipsum";

        public static void EnsureDBCreatedWithSampleWord(IServiceProvider services)
        {
            var env = services.GetRequiredService<IHostingEnvironment>();
            if (!env.IsDevelopment())
                return;

            using (var context = services.GetRequiredService<HtmlToWordDbContext>())
            {
                context.Database.EnsureCreated();

                if (context.WordDictionary.Any())
                    return;

                var hashingProvider = services.GetRequiredService<IHashProvider>();
                var salt = hashingProvider.GenerateSalt();
                var hash = hashingProvider.GenerateSHA256Hash(Word, salt);

                var encryptionProvider = services.GetRequiredService<IEncryptionProvider>();
                var encryptedWord = encryptionProvider.Encrypt(Word);

                context.WordDictionary.Add(new WordDictionary
                {
                    Id = hash.ToHexString(),
                    Salt = hash.ToHexString(),
                    Word = encryptedWord,
                    Count = 0
                });

                var passWordSalt = hashingProvider.GenerateSalt();
                var passwordHash = hashingProvider.GeneratePBKDF2Hash("admin", salt);
                context.Users.Add(new User
                {
                    FirstName = "Admin",
                    LastName = "Test",
                    UserName = "Admin",
                    NormalisedUserName = "ADMIN",
                    Email = "admin@test.co",
                    Salt = salt.ToHexString(),
                    Password = passwordHash.ToHexString()
                });

                context.SaveChanges();

                var word = context.WordDictionary.SingleOrDefault(x => x.Word == encryptedWord);
            }
        }
    }
}
