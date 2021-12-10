using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CourseEindcase.DTO;
using CourseEindcase.Model;
using CourseEindcase.Parsers;
using CourseEindcase.Repositories;
using CourseEindcase.Services;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace CourseEindcase.UnitTests.Services;

public class CourseImport
{
    [Fact]
    public async Task ImportCourses_ShouldImport_WhenNoDuplicateCourseFound()
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

        List<Course> courses = new List<Course>()
        {
            new Course()
            {
                Title = "Object Oriented Programming in C# By Example",
                CourseCode = "OOCS",
                Duration = 5,
                Editions = new List<CourseEdition>()
                {
                    new CourseEdition()
                    {
                        StartDatum = new DateTime(2021, 3, 22)
                    },
                    new CourseEdition()
                    {
                        StartDatum = new DateTime(2021, 3, 27)
                    }
                }
            },
            new Course()
            {
                Title = "Object Oriented Programming in C# By Example 2",
                CourseCode = "OOCS2",
                Duration = 5,
                Editions = new List<CourseEdition>()
                {
                    new CourseEdition()
                    {
                        StartDatum = new DateTime(2021, 3, 29)
                    }
                }
            }
        };
        var mockRepository = new Mock<ICoursesImportRepository>();
        var mockParser = new Mock<ICourseParser>();
        var coursesImporterService = new CourseImportService(mockRepository.Object, mockParser.Object);
        mockParser.Setup(b => b.Parse(It.IsAny<string>())).Returns(courses);
        mockRepository.Setup(mr => mr.GetCourseWithCourseEditions(It.IsAny<Course>())).Returns(Task.FromResult<Course>(null));
        ImportReply reply = await coursesImporterService.ImportCourses(file);

        Assert.Equal(2, reply.CoursesAdded);
        Assert.Equal(3, reply.EditionsAdded);
    }

    [Fact]
    public async Task ImportCourses_ShouldNotImportDuplicates()
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


            Course course1 = new Course()
            {
                Id = 1,
                Title = "Object Oriented Programming in C# By Example",
                CourseCode = "OOCS",
                Duration = 5,
                Editions = new List<CourseEdition>()
                {
                    new CourseEdition()
                    {
                        StartDatum = new DateTime(2021, 3, 22)
                    },
                    new CourseEdition()
                    {
                        StartDatum = new DateTime(2021, 3, 29)
                    }
                }
            };
            Course course2 =  new Course()
                {
                    Id = 2,
                    Title = "LINQ: .NET Language-Integrated Query",
                    CourseCode = "LINQ",
                    Duration = 2,
                    Editions = new List<CourseEdition>()
                    {
                        new CourseEdition()
                        {
                            StartDatum = new DateTime(2021, 3, 29)
                        }
                    }
                };
            
            Course existingCourse = new Course()
            {
                Title = "LINQ: .NET Language-Integrated Query",
                CourseCode = "LINQ",
                Duration = 2,
                Editions = new List<CourseEdition>()
                {
                    new CourseEdition()
                    {
                        StartDatum = new DateTime(2021, 3, 29)
                    }
                }
            };

            List<Course> newCourses = new List<Course>() { course1, course2 };
            var mockRepository = new Mock<ICoursesImportRepository>();
            var mockParser = new Mock<ICourseParser>();
            var coursesImporterService = new CourseImportService(mockRepository.Object, mockParser.Object);
            mockRepository.Setup(mr => mr.GetCourseWithCourseEditions(course2)).ReturnsAsync(existingCourse);
            mockRepository.Setup(crm => crm.GetCourseWithCourseEditions(course1)).Returns(Task.FromResult<Course>(null));
            mockParser.Setup(b => b.Parse(It.IsAny<string>())).Returns(newCourses);
            ImportReply reply = await coursesImporterService.ImportCourses(file);

            Assert.Equal(1, reply.CoursesAdded);
            Assert.Equal(2, reply.EditionsAdded);
            Assert.Equal(1, reply.DuplicateCourses);
            Assert.Equal(1, reply.DuplicateEditions);
        }
}