using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class HtmlParser : IHtmlParser
    {
        private const int MaxWordLengthToRemove = 2;

        public List<IFilter> DefaultFilters { get; }

        public HtmlParser()
        {
            DefaultFilters = new List<IFilter> {
                new HtmlTagsFilter(),
                new AlphaNumericFilter(),
                new LengthFilter(MaxWordLengthToRemove),
                new SpecialCharactersFilter(),
                new StopWordsFilter(new DefaultStopWordProvider()),
                new ExtraSpacesFilter() };
        }

        public IEnumerable<string> ExtractWords(string text)
        {
            var result = text;

            foreach (var filter in DefaultFilters)
                result = filter.Execute(result);

            return result.Split(" ")
                         .Except(string.IsNullOrEmpty);
        }
    }
}
