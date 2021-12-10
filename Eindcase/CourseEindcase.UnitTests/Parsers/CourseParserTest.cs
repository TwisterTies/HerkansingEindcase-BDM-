using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CourseEindcase.Model;
using CourseEindcase.Parsers;
using Xunit;
using Moq;

namespace CourseEindcase.UnitTests.Parsers;

public class CourseParserTest
{
    [Fact]
    public void ShouldParseFile()
    {
        string testFile =
$@"Titel: Object Oriented Programming in C# By Example
Cursuscode: OOCS
Duur: 5 dagen
Startdatum: 22/03/2021

Titel: Object Oriented Programming in C# By Example
Cursuscode: OOCS
Duur: 5 dagen
Startdatum: 29/03/2021

Titel: LINQ: .NET Language-Integrated Query
Cursuscode: LINQ
Duur: 2 dagen
Startdatum: 22/03/2021";

        List<Course> courses =  InitCourseImporter().Parse(testFile);
        
        Assert.Collection(courses,
            c =>
            {
                Assert.Equal("OOCS", c.CourseCode);
                Assert.Equal("Object Oriented Programming in C# By Example", c.Title);
                Assert.Equal(5, c.Duration);
                Assert.Collection(c.Editions, edition =>
                    {
                        Assert.Equal(new DateTime(2021, 03, 22), edition.StartDatum);
                    },
                    edition =>
                    {
                        Assert.Equal(new DateTime(2021, 03, 29), edition.StartDatum);
                    });
            },
            c => 
            {
                Assert.Equal("LINQ", c.CourseCode);
                Assert.Equal("LINQ: .NET Language-Integrated Query", c.Title);
                Assert.Equal(2, c.Duration);
                Assert.Collection(c.Editions, edition =>
                    {
                        Assert.Equal(new DateTime(2021, 03, 22), edition.StartDatum);
                    });
            });
    }
    private static CourseParser InitCourseImporter() 
    {
        var titleParserMock = new Mock<ICoursePropertyParser<string>>(); 
        var codeParserMock = new Mock<ICoursePropertyParser<string>>(); 
        var durationParserMock = new Mock<ICoursePropertyParser<int>>(); 
        var startDateParserMock = new Mock<ICoursePropertyParser<DateTime>>(); 
        var emptyLineParserMock = new Mock<ICoursePropertyParser<string>>();
        
        titleParserMock.Setup(p => p.Parse("Titel: Object Oriented Programming in C# By Example")).Returns("Object Oriented Programming in C# By Example"); 
        titleParserMock.Setup(p => p.Parse("Titel: LINQ: .NET Language-Integrated Query")).Returns("LINQ: .NET Language-Integrated Query");
        
        codeParserMock.Setup(p => p.Parse("Cursuscode: OOCS")).Returns("OOCS"); 
        codeParserMock.Setup(p => p.Parse("Cursuscode: LINQ")).Returns("LINQ");
        
        durationParserMock.Setup(p => p.Parse("Duur: 5 dagen")).Returns(5); 
        durationParserMock.Setup(p => p.Parse("Duur: 2 dagen")).Returns(2);
        
        startDateParserMock.Setup(p => p.Parse("Startdatum: 22/03/2021")).Returns(new DateTime(2021, 03, 22)); 
        startDateParserMock.Setup(p => p.Parse("Startdatum: 29/03/2021")).Returns(new DateTime(2021, 03, 29));
        
        emptyLineParserMock.Setup(p => p.Parse("")).Returns("");
        
        var parser = new CourseParser(titleParserMock.Object, codeParserMock.Object, durationParserMock.Object, startDateParserMock.Object, emptyLineParserMock.Object);
        
        return parser;
    }
}