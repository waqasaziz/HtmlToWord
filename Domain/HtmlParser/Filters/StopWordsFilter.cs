using System.Text.RegularExpressions;

namespace Domain
{
    /// <summary>
    /// Removes English stop words
    /// Uses Regex Expression from => https://lotsacode.wordpress.com/2010/10/08/remove-google-stopwords-from-string/
    /// </summary>
    public class StopWordsFilter : IFilter
    {
        private readonly IStopWordProvider _stopwordProvider;

        public StopWordsFilter(IStopWordProvider stopwordProvider)
        {
            _stopwordProvider = stopwordProvider;
        }
        public string Execute(string text)
        {
            var regexCode = @"(?<=(\A|\s|\.|,|!|\?))(" + string.Join("|", _stopwordProvider.GetStopWords()) + @")(?=(\s|\z|\.|,|!|\?))";

            return Regex.Replace(text, regexCode, " ", RegexOptions.Singleline | RegexOptions.IgnoreCase).TrimExtraSpaces();
        }
    }
}
