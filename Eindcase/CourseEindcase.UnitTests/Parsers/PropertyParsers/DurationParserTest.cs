using CourseEindcase.Parsers.CoursePropertyParsers;
using Xunit;
namespace CourseEindcase.UnitTests.Parsers.PropertyParsers;

public class DurationParserTest
{
    [Fact]
    public void Parse_ValidDuration_ReturnsDuration()
    {
        // Arrange
        string input = "Duur: 2 dagen";
        int expected = 2;
        CourseDurationParser durationParser = new CourseDurationParser();
        
        // Act
        var result = durationParser.Parse(input);

        // Assert
        Assert.Equal(expected, result);
    }
}