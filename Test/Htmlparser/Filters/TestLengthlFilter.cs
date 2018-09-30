using Domain;
using Xunit;

namespace Tests
{
    public class TestLengthlFilter
    {
        [Fact]
        public void Removes_Words_Equal_To_Two_Characters()
        {
            var text = "XY";

            var filter = new LengthFilter(2);

            var result = filter.Execute(text).Trim();

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Removes_Words_Less_ThanTwo_Characters()
        {
            var text = "X";

            var filter = new LengthFilter(2);

            var result = filter.Execute(text).Trim();

            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Ignores_Words_Greater_Than_Two_Characters()
        {
            var text = "XYZ";

            var filter = new LengthFilter(2);

            var result = filter.Execute(text).Trim();

            Assert.Equal(text, result);
        }
    }
}
