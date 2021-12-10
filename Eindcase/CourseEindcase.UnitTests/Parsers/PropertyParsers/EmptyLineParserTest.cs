using CourseEindcase.Parsers.CoursePropertyParsers;
using Xunit;

namespace CourseEindcase.UnitTests.Parsers.PropertyParsers;

public class EmptyLineParserTest
{
    [Fact]
    public void Parse_EmptyLine_ReturnsEmpty()
    {
        // Arrange
        var parser = new EmptyParser();
        var input = "";
        var expected = "";
        // Act
        var result = parser.Parse(input);

        // Assert
        Assert.Equal(expected, result);
    }
}