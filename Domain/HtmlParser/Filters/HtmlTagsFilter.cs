using System.Text.RegularExpressions;

namespace Domain
{
    /// <summary>
    /// Removes html Tags
    /// </summary>
    public class HtmlTagsFilter : IHtmlFilter
    {
        public string Filter(string text)
        {
            return Regex.Replace(text, "<[^>]+>", " ", RegexOptions.IgnoreCase);
        }
    }
}
