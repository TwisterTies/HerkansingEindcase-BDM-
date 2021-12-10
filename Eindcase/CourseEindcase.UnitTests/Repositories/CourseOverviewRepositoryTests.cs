using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseEindcase.Data;
using CourseEindcase.DTO;
using CourseEindcase.Model;
using CourseEindcase.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using Xunit;
using Moq;

namespace CourseEindcase.UnitTests.Repositories;

public class CourseOverviewRepositoryTests : DBContextBase
{
    [Fact]
    public async Task GetOverviewOfAllCourses_ByStartDate()
    {
        var testCourse1 = new Course()
                {
                CourseCode = "CODE1", Duration = 1, Title = "Course1", Editions = new List<CourseEdition>()
                {
                    new CourseEdition() { StartDatum = new DateTime(2021, 10, 15) }
                }
                };
                var testCourse2 = new Course()
                {
                    CourseCode = "CODE2", Duration = 3, Title = "Course2", Editions = new List<CourseEdition>()
                    {
                        new CourseEdition() { StartDatum = new DateTime(2021, 10, 16) }
                    }
                };
                var testCourse3 = new Course()
                {
                    CourseCode = "CODE3", Duration = 2, Title = "Course3", Editions = new List<CourseEdition>()
                    {
                        new CourseEdition() { StartDatum = new DateTime(2021, 10, 17) }
                    }
                };

                using (var context = new CaseContext(ContextOptions))
                {
                    await context.Database.EnsureCreatedAsync();
                    await context.Database.EnsureDeletedAsync();
                    context.AddRange(testCourse1, testCourse2, testCourse3);
                    await context.SaveChangesAsync();
                }
    
        using (var context = new CaseContext(ContextOptions))
        {
            CourseOverviewRepository repo = new CourseOverviewRepository(context); 
            IEnumerable<CourseOverview> courses = await repo.GetCoursesOverview(); 
            Assert.Collection(courses, c =>
                {
                    Assert.Equal(testCourse1.Title, c.Title);
                    Assert.Equal(testCourse1.Duration, c.Duration);
                    Assert.Equal(testCourse1.Editions.First().StartDatum, c.StartDate);
                },           
                c=> {
                    Assert.Equal(testCourse2.Title, c.Title);
                    Assert.Equal(testCourse2.Duration, c.Duration);
                    Assert.Equal(testCourse2.Editions.First().StartDatum, c.StartDate);
                },
                c=>{
                    Assert.Equal(testCourse3.Title, c.Title);
                    Assert.Equal(testCourse3.Duration, c.Duration);
                    Assert.Equal(testCourse3.Editions.First().StartDatum, c.StartDate);
                });
        }
    }
}