namespace Domain.Services
{
    using Repositories;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class WordDictionaryService : IWordDictionaryService
    {
        private readonly HttpClient _httpClient;
        private readonly IHtmlParser _htmlParser;
        private readonly IWordDictionaryRepository _repository;

        public WordDictionaryService(IHtmlParser htmlParser, HttpClient httpClient, IWordDictionaryRepository repository)
        {
            _httpClient = httpClient;
            _htmlParser = htmlParser;
            _repository = repository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">URL to html page</param>
        /// <param name="top">Number of top most used words to fetch</param>
        /// <returns></returns>
        public async Task<Dictionary<string, int>> GetWordsAsync(string url, int top)
        {
            var result = new ConcurrentDictionary<string, int>();

            var contents = await _httpClient.GetStringAsync(url);

            var words = _htmlParser.ExtractWords(contents).ToList();
            words.AsParallel().ForAll(word => result.AddOrUpdate(word, 1, (k, v) => v + 1));

            var wordsDictionary = result.OrderByDescending(x => x.Value)
                                        .Take(top)
                                        .ToDictionary(entry => entry.Key, entry => entry.Value);

            await _repository.SaveWordsAsync(wordsDictionary);

            return wordsDictionary;
        }
    }
}
