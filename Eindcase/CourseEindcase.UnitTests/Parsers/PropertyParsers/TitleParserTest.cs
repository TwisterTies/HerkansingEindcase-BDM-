using CourseEindcase.Parsers.CoursePropertyParsers;
using Xunit;

namespace CourseEindcase.UnitTests.Parsers.PropertyParsers;

public class TitleParserTest
{
    [Fact]
    public void Parse_Title_ReturnsTitle()
    {
        string input = "Titel: LINQ Programming For Dummies";
        string expected = "LINQ Programming For Dummies";
        CourseTitleParser titleParser = new CourseTitleParser();
        string result = titleParser.Parse(input);

        Assert.Equal(expected, result);
    }
}