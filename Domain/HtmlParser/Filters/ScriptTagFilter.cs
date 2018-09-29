using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{
    public class ScriptTagFilter : IHtmlFilter
    {
        private const string ScriptTag = "script";

        public string Filter(string text)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(text);

            doc.DocumentNode.Descendants()
                            .Where(n => n.Name.Equals(ScriptTag, StringComparison.OrdinalIgnoreCase))
                            .ToList()
                            .ForEach(n => n.Remove());

            return doc.DocumentNode.OuterHtml;
        }
    }
}
