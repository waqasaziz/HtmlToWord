using System.Text.RegularExpressions;

namespace Domain
{
    /// <summary>
    /// Removes all aplha numeric words from given string
    /// </summary>
    public class AlphaNumericFilter : IFilter
    {
        public string Execute(string text) => Regex.Replace(text, @"(^[a-zA-Z0-9]*$)|([\d-])", " ").TrimExtraSpaces();
    }
}
