using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Domain
{
    public class SpecialCharactersFilter : IFilter
    {
        public string Execute(string text) => Regex.Replace(text, @"[^\w\s]", " ").Replace("_", " ").TrimExtraSpaces();
    }
}
