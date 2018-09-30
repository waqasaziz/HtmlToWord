using Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests
{
    public class TestHTMLParser
    {
        [Fact]
        public void Extracts_Words()
        {
            var html = BuildHtml().ToString();

            var parser = new HtmlParser();

            var result = string.Join(" ",parser.ExtractWords(html));

            var expectedResult = "Unordered HTML List Unordered HTML List Coffee Tea Milk Unordered HTML List stopwords punctuation email com";
            Assert.Equal(expectedResult, result);
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

            result.Append("<h2>An Unordered HTML List</h2>");
            result.Append("<img src='my.jpg' alt='my.com' width='104' height='142'>");

            result.Append("<h2>An Unordered HTML List</h2>");

            result.Append("<ul>");
            result.Append("<li>Coffee</li>");
            result.Append("<li>Tea</li>");
            result.Append("<li>Milk</li>");
            result.Append("</ul>");

            result.Append("<p>An Unordered HTML List of stopwords and punctuation:</p>");
            result.Append("<ul>");
            result.Append("<li>You</li>");
            result.Append("<li>They</li>");
            result.Append("<li>the</li>");
            result.Append("<li>my@email.com</li>");

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
