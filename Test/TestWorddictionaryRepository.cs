using Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class TestWordDictionaryRepository
    {
        private Dictionary<string, int> WordsDictionary
        {
            get
            {
                return new Dictionary<string, int>
                    {
                        { "Lorem", 10 },
                        { "Ipsum", 20 },
                        { "Accusamus", 30 },
                        { "Voluptatem", 40 },
                        { "Voluptate", 50 }
                    };
            }
        }

        [Fact]
        public async Task Can_Save_Words()
        {
            using (var context = TestsHelper.CreateInMemoryDbContext())
            {
                var repository = new WordDictionaryRepository(context, TestsHelper.HasingProvider, TestsHelper.EncryptionProvider);

                await repository.SaveWordsAsync(WordsDictionary);

                Assert.True(context.WordDictionary.Count() == 5);
            }
        }

        [Fact]
        public async Task Can_Load_In_Descending_order()
        {
            var LowestUsedWord = WordsDictionary.First().Key;
            var HighestUsedWord = WordsDictionary.Last().Key;

            using (var context = TestsHelper.CreateInMemoryDbContext())
            {
                var repository = new WordDictionaryRepository(context, TestsHelper.HasingProvider, TestsHelper.EncryptionProvider);

                await repository.SaveWordsAsync(WordsDictionary);

                var result = repository.GetAll();

                Assert.Equal(result.First().Key, HighestUsedWord);
                Assert.Equal(result.Last().Key, LowestUsedWord);

            }
        }

    }
}
