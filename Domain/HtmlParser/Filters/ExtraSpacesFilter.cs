using System.Text.RegularExpressions;

namespace Domain
{
    /// <summary>
    /// Removes extra spaces from given string
    /// </summary>
    public class ExtraSpacesFilter : IFilter
    {
        public string Execute(string text) => Regex.Replace(text, @"\s+", " ").TrimExtraSpaces();
    }
}
