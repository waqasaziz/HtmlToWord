using System.Text.RegularExpressions;

namespace Domain
{
    /// <summary>
    /// Removes all aplha numeric words from given string
    /// </summary>
    public class AlphaNumericFilter : IHtmlFilter
    {
        public string Filter(string text) => Regex.Replace(text, @"(\d\w*)|([^a-zA-Z -])", " ");
    }
}
