using HtmlAgilityPack;
using System;
using System.Linq;
using System.Text;

namespace Domain
{
    /// <summary>
    /// Removes html Tags
    /// </summary>
    public class HtmlTagsFilter : IFilter
    {
        private const string StyleTag = "style";
        private const string ScriptTag = "script";
        private readonly HtmlDocument _htmlDocument;

        public HtmlTagsFilter()
        {
            _htmlDocument = new HtmlDocument();
        }

        /// <summary>
        /// Removes style tags
        /// Removes script tags
        /// Removes all HTML tags and extract inner text
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string Execute(string text)
        {
            _htmlDocument.LoadHtml(text);

            RemoveStyleTags();

            RemoveScriptTags();

            return RemoveHTmlTags();
        }

        private void RemoveStyleTags()
        {
            _htmlDocument.DocumentNode.Descendants()
                            .Where(n => n.Name.Equals(StyleTag, StringComparison.OrdinalIgnoreCase))
                            .ToList()
                            .ForEach(n => n.Remove());
        }

        private void RemoveScriptTags()
        {
            _htmlDocument.DocumentNode.Descendants()
                           .Where(n => n.Name.Equals(ScriptTag, StringComparison.OrdinalIgnoreCase))
                           .ToList()
                           .ForEach(n => n.Remove());
        }

        private string RemoveHTmlTags()
        {
            var result = new StringBuilder();

            foreach (var node in _htmlDocument.DocumentNode.SelectNodes("//body//text()"))
                result.AppendFormat(" {0} ", node.InnerText);

            return result.ToString().TrimExtraSpaces();
        }
    }
}
