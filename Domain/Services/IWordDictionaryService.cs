
namespace Domain.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWordDictionaryService
    {
        Task<Dictionary<string, int>> GetWordsAsync(string url, int top);
    }
}
