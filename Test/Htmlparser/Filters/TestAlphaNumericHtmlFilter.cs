using Domain;
using Xunit;

namespace Tests
{
    public class TestAlphaNumericHtmlFilter
    {
        [Fact]
        public void Removes_Numerics()
        {
            var text = "XY 160";

            var filter = new AlphaNumericFilter();

            var result = filter.Execute(text).Trim();

            Assert.Equal("XY", result);
        }

        [Fact]
        public void Removes_Words_Starting_with_Numbers()
        {
            var text = "88XY";

            var filter = new AlphaNumericFilter();

            var result = filter.Execute(text).Trim();

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Removes_Words_Shuffled_With_Numbers()
        {
            var text = "XY88X1Y";

            var filter = new AlphaNumericFilter();

            var result = filter.Execute(text).Trim();

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Removes_Words_Tariling_With_Numbers()
        {
            var text = "XY88";

            var filter = new AlphaNumericFilter();

            var result = filter.Execute(text).Trim();

            Assert.Equal(string.Empty, result);
        }
    }
}
