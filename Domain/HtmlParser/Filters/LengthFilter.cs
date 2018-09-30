using System.Text.RegularExpressions;

namespace Domain
{
    /// <summary>
    /// Removes words of given length
    /// </summary>
    public class LengthFilter : IFilter
    {
        private readonly int _maxWordlength;

        /// <summary>
        /// length of the word to be removed from given string
        /// </summary>
        /// <param name="MaxWordlength"></param>
        public LengthFilter(int MaxWordlength)
        {
            _maxWordlength = MaxWordlength;
        }

        public string Execute(string text) => Regex.Replace(text, "(\\b\\w{1,"+_maxWordlength+"}\\b)", " ").TrimExtraSpaces();
    }
}
