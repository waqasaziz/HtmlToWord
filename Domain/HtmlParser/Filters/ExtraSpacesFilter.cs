using System.Text.RegularExpressions;

namespace Domain
{
    /// <summary>
    /// Removes extra spaces from given string
    /// </summary>
    public class ExtraSpacesFilter : IHtmlFilter
    {
        public string Filter(string text) => Regex.Replace(text, @"\s+", " ");
    }
}
