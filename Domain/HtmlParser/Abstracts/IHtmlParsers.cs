using System.Collections.Generic;

namespace Domain
{
    public interface IHtmlParser
    {
        List<IFilter> DefaultFilters { get; }

        IEnumerable<string> ExtractWords(string text);
    }
}
