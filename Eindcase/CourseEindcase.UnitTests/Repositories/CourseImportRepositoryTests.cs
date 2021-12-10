using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseEindcase.Data;
using CourseEindcase.Model;
using CourseEindcase.Repositories;
using Xunit;

namespace CourseEindcase.UnitTests.Repositories;

public class CourseImportRepositoryTests : DBContextBase
{
    [Fact]
    public async Task GetCourseWithEdition_ReturnsExistingCourse()
    {
        Course course1 = new Course() {
            CourseCode = "LINQ", Duration = 1, Title = "Linq Queries For Dummies",
            Editions = new List<CourseEdition>() {
                new CourseEdition() { StartDatum = new DateTime(2021, 03, 15) }
            }
        };
        using (var context = new CaseContext(ContextOptions)) {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
            Course course2 = new Course() {
                CourseCode = "C#", Duration = 3, Title = "C# Programming for Dummies",
                Editions = new List<CourseEdition>() {
                    new CourseEdition() { StartDatum = new DateTime(2002, 02, 02) }
                }
            };

            context.Courses.AddRange(course1, course2);
            await context.SaveChangesAsync();
        }

        using (var context = new CaseContext(ContextOptions)) {
            CourseImportRepository repository = new CourseImportRepository(context);
            Course course = await repository.GetCourseWithCourseEditions(course1);
            Assert.Equal(course1.Id, course.Id);
            Assert.Collection(course1.Editions, e => Assert.Equal(1, e.Id));
        }
    }
}
