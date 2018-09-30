using Domain;
using Xunit;

namespace Tests
{
    public class TestSpecialCharacterFilter
    {
        [Fact]
        public void Removes_Punctuation()
        {
            var text = "Comma, semicolon; Colon:";

            var filter = new SpecialCharactersFilter();

            var result = filter.Execute(text).Trim();

            Assert.Equal("Comma semicolon Colon", result);
        }

        [Fact]
        public void Removes_Symbols()
        {
            var text = "Underscore_AtRate@ .Dot Pound£ Dollar$ Percent% Mod^ And& Star* Hash# Open(Close)";

            var filter = new SpecialCharactersFilter();

            var result = filter.Execute(text).Trim();

            Assert.Equal("Underscore AtRate Dot Pound Dollar Percent Mod And Star Hash Open Close", result);
        }
    }
}
