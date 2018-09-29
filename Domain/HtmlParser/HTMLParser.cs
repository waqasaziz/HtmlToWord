using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class HtmlParser : IHtmlParser
    {
        private const int MaxWordLengthToRemove = 2;

        public List<IHtmlFilter> DefaultFilters { get; }

        public HtmlParser()
        {
            DefaultFilters = new List<IHtmlFilter> {
                new ScriptTagFilter(),
                new StyleTagFilter(),
                new AlphaNumericFilter(),
                new LengthFilter(MaxWordLengthToRemove),
                new StopWordsFilter(new DefaultStopWordProvider()),
                new ExtraSpacesFilter() };
        }

        public IEnumerable<string> ExtractWords(string text)
        {
            var result = text;

            foreach (var filter in DefaultFilters)
                result = filter.Filter(result);

            return result.Split(" ")
                         .Except(string.IsNullOrEmpty)
                         .OrderBy(x => x);
        }
    }
}
