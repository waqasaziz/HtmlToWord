using Domain;
using Xunit;

namespace Tests
{
    public class TestStopwordFilter
    {
        [Fact]
        public void Removes_Stopswords()
        {
            var text = "A quick brown fox jumps over the lazy dog";

            var filter = new StopWordsFilter(new DefaultStopWordProvider());

            var result = filter.Execute(text).Trim();

            Assert.Equal("quick brown fox jumps lazy dog", result);
        }

       

        [Fact]
        public void Removes_Articles()
        
{
            var text = "Article A Article The";

            var filter = new StopWordsFilter(new DefaultStopWordProvider());

            var result = filter.Execute(text).Trim();

            Assert.Equal("Article Article", result);
        }
    }
}
