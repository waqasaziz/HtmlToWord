namespace Domain.Data
{
    using Microsoft.Extensions.DependencyInjection;
    using Security;
    using System;
    using System.Linq;

    public static class SeedData
    {
        private const string Word = "Lorem Ipsum";

        public static void EnsureDBCreatedWithSampleWord(IServiceProvider services)
        {
            using (var context = services.GetRequiredService<HtmlToWordDbContext>())
            {
                context.Database.EnsureCreated();

                if (context.WordDictionary.Any())
                    return;

                var hashingProvider = services.GetRequiredService<IHashProvider>();
                var salt = hashingProvider.GenerateSalt();
                var hash = hashingProvider.GenerateHash(Word, salt);

                context.WordDictionary.Add(new WordDictionary
                {
                    Id = hash.ToHexString(),
                    Salt = hash.ToHexString(),
                    Word = Word,
                    Count = 0
                });

                context.SaveChanges();
            }
        }
    }
}
