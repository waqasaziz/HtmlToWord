using HtmlAgilityPack;
using System;
using System.Linq;

namespace Domain
{

    public class StyleTagFilter : IHtmlFilter
    {
        private const string StyleTag = "style";

        public string Filter(string text)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(text);

            doc.DocumentNode.Descendants()
                            .Where(n => n.Name.Equals(StyleTag, StringComparison.OrdinalIgnoreCase))
                            .ToList()
                            .ForEach(n => n.Remove());

            return doc.DocumentNode.OuterHtml;
        }
    }
}
