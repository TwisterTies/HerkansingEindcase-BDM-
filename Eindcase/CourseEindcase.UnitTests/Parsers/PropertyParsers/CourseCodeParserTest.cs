using CourseEindcase.Parsers.CoursePropertyParsers;
using Xunit;

namespace CourseEindcase.UnitTests.Parsers.PropertyParsers;

public class CourseCodeParserTest
{
    [Fact]
    public void Parse_ValidInput_ReturnsCourseCode()
    {
        // Arrange
        var input = "Cursuscode: LINQ";
        var expected = "LINQ";

        // Act
        CourseCodeParser parser = new CourseCodeParser();
        var result = parser.Parse(input);

        // Assert
        Assert.Equal(expected, result);
    }
}