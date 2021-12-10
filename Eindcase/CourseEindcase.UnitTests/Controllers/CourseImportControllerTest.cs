using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CourseEindcase.Controllers;
using CourseEindcase.DTO;
using CourseEindcase.Interfaces;
using CourseEindcase.Model;
using Microsoft.AspNetCore.Http;
using Xunit;
using Moq;

namespace CourseEindcase.UnitTests.Controllers;

public class CourseImportControllerTest
{
    Mock<ICoursesImportService> _coursesImportService;

    public CourseImportControllerTest()
    {
        _coursesImportService = new Mock<ICoursesImportService>();
    }
    
    [Fact]
    public async Task ImportCourse_ShouldReturn_ReplyFromService()
    {
        var formFileContent = $@"Titel: Object Oriented Programming in C# By Example
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
        
        byte[] fileBytes = Encoding.UTF8.GetBytes(formFileContent);
        
        IFormFile file = new FormFile(new MemoryStream(fileBytes), 0, fileBytes.Length, "file", "dummy.txt")
        {
            Headers = new HeaderDictionary(),
            ContentType = "text/plain",
            ContentDisposition = "form-data; name=\"file\"; filename=\"dummy.txt\"",
        };
        ImportReply expectedReply = new ImportReply()
        {
            CoursesAdded = 2,
            EditionsAdded = 3
        };
        
        _coursesImportService.Setup(x => x.ImportCourses(file)).ReturnsAsync(expectedReply);
        
        var controller = new CourseImportController(_coursesImportService.Object);
        
        ImportReply actualReply = await controller.ImportFile(file);
        
        Assert.Equal(expectedReply, actualReply);
    }
}