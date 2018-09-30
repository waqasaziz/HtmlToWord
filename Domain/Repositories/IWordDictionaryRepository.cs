namespace Domain.Repositories
{
    using Domain.Data;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWordDictionaryRepository:  IDisposable
    {
        Task SaveWordsAsync(Dictionary<string, int> wordsDictionary);

        Dictionary<string, int> GetAll();
    }
}
