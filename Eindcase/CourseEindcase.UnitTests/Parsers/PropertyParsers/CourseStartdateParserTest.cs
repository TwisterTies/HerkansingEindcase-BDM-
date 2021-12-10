using System;
using CourseEindcase.Parsers.CoursePropertyParsers;
using Xunit;

namespace CourseEindcase.UnitTests.Parsers.PropertyParsers;

public class CourseStartdateParserTest
{
    [Fact]
    public void Parse_ValidInput_ReturnsCorrectDate()
    {
        // Arrange
        var input = "Startdatum: 01/01/2020";
        CourseStartdateParser parser = new CourseStartdateParser();
        var expected = new DateTime(2020, 1, 1);
        
        // Act
        var result = parser.Parse(input);

        // Assert
        Assert.Equal(expected, result);
    }
}