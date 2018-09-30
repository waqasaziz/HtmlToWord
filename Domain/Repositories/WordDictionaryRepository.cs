namespace Domain.Repositories
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Security;
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class WordDictionaryRepository : IWordDictionaryRepository, IDisposable
    {
        private readonly HtmlToWordDbContext _context;
        private readonly IHashProvider _hashProvider;
        private readonly IEncryptionProvider _encryptionProvier;

        public WordDictionaryRepository(HtmlToWordDbContext context, IHashProvider hashProvider, IEncryptionProvider encryptionProvier)
        {
            _context = context;
            _hashProvider = hashProvider;
            _encryptionProvier = encryptionProvier;
        }

        private async Task Add(KeyValuePair<string, int> word)
        {
            var salt = _hashProvider.GenerateSalt();
            var hash = _hashProvider.GenerateSHA256Hash(word.Key, salt);
            var encryptedWord = _encryptionProvier.Encrypt(word.Key.Trim().ToUpperFirstLetter());
            await _context.WordDictionary.AddAsync(new WordDictionary
            {
                Id = hash.ToHexString(),
                Salt = salt.ToHexString(),
                Word = encryptedWord,
                Count = word.Value
            });

        }

        public async Task SaveWordsAsync(Dictionary<string, int> wordsDictionary)
        {
            foreach (var word in wordsDictionary)
                await Add(word);

            await _context.SaveChangesAsync();
        }

        public Dictionary<string, int> GetAll()
        {
            var words = _context.WordDictionary
                    .AsNoTracking()
                    .ToList();

            var result = new ConcurrentDictionary<string, int>();

            words.AsParallel()
                .ForAll(item => result.AddOrUpdate(_encryptionProvier.Decrypt(item.Word), 1, (k, v) => v + item.Count));

            return result.OrderByDescending(x => x.Value).ToDictionary(entry => entry.Key, entry => entry.Value);

        }

        public void Dispose() => _context.Dispose();

    }
}
