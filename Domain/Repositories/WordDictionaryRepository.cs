namespace Domain.Repositories
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Security;
    using System;
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

        private async Task AddOrUpdate(KeyValuePair<string, int> word)
        {
            //Encrypt Word
            var encryptedWord = _encryptionProvier.Encrypt(word.Key.ToUpperFirstLetter());

            //Find existing
            var existingWord =  await _context.WordDictionary.SingleOrDefaultAsync(x => x.Word == encryptedWord);

            // If exists then update and return 
            if (existingWord != null)
            {
                existingWord.Count += word.Value;
                _context.WordDictionary.Update(existingWord);

                return;
            }

            // Add new word
            var salt = _hashProvider.GenerateSalt();
            var hash = _hashProvider.GenerateHash(word.Key, salt);
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
                await AddOrUpdate(word);

            await _context.SaveChangesAsync();
        } 

        public Dictionary<string, int> GetAll()
        {
            return _context.WordDictionary
                    .OrderByDescending(x => x.Count)
                    .AsNoTracking()
                    .ToDictionary(entry => _encryptionProvier.Decrypt(entry.Word), entry => entry.Count);

        }

        public void Dispose() => _context.Dispose();

    }
}
