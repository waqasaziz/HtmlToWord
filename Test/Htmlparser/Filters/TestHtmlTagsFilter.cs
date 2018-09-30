using Domain;
using System.Text;
using Xunit;

namespace Tests
{
    public class TestHtmlTagsFilter
    {
        [Fact]
        public void Removes_HtmlTags()
        {
            var html = BuildHtml().ToString();

            var filter = new HtmlTagsFilter();

            var result = filter.Execute(html).Trim();

            Assert.Equal("Coffee Tea Milk", result);
        }

        private static StringBuilder BuildHtml()
        {
            var result = new StringBuilder();

            result.Append("<!DOCTYPE html>");
            result.Append("<html>");

            result.Append("<head>");
            result.Append("<style>");
            result.Append("{.body{width:100%, height:100%;} p{color:red;}}");
            result.Append("</style>");

            result.Append("</head>");
            result.Append("<body>");

            result.Append("<img src='my.jpg' alt='my.com' width='104' height='142'>");
            result.Append("<ul>");
            result.Append("<li>Coffee</li>");
            result.Append("<li><p>Tea</p></li>");
            result.Append("<li><a>Milk</a></li>");
            result.Append("</ul>");

            result.Append("<script type='text/javascript'>");
            result.Append("document.elementByQuery('#button')");
            result.Append("</script>");

            result.Append("</body>");
            result.Append("</html>");

            return result;
        }
    }
}
