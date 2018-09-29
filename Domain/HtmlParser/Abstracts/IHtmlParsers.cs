using System.Collections.Generic;

namespace Domain
{
    public interface IHtmlParser
    {
        List<IHtmlFilter> DefaultFilters { get; }

        IEnumerable<string> ExtractWords(string text}
    }
}
