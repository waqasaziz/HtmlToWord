using Domain;
using Domain.Repositories;
using Domain.Services;
using Moq;
using Moq.Protected;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class TestWordDicrionaryService
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
        public async Task Can_Return_words_By_Top()
        {
            var htmlParser = SetupHtmlParser();
            var httpClient = SetupHttpClient();
            var repository = SetupRepository();

            var service = new WordDictionaryService(htmlParser, httpClient, repository);
            var result = await service.GetWordsAsync("Http://mycustomurl.com", 3);

            Assert.True(result.Count == 3);
        }

        private IWordDictionaryRepository SetupRepository()
        {
            var mockRepository = new Mock<IWordDictionaryRepository>();

            mockRepository.Setup(x => x.SaveWordsAsync(WordsDictionary))
                           .Returns(Task.CompletedTask);

            mockRepository.Setup(x => x.GetAll())
                           .Returns(WordsDictionary);

            return mockRepository.Object;

        }

        private IHtmlParser SetupHtmlParser()
        {
            var mockParser = new Mock<IHtmlParser>();

            mockParser.Setup(x => x.ExtractWords(It.IsAny<string>()))
                  .Returns(new List<string> { "Lorem", "Ipsum", "Accusamus", "Voluptatem", "Voluptate" });

            return mockParser.Object;
        }

        private HttpClient SetupHttpClient()
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = HttpStatusCode.OK,
                   Content = new StringContent(string.Empty),
               })
               .Verifiable();

            // use real http client with mocked handler here
            return new HttpClient(handlerMock.Object);

        }
    }
}
