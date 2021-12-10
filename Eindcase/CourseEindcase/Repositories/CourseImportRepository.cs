using CourseEindcase.Data;
using CourseEindcase.Model;
using Microsoft.EntityFrameworkCore;

namespace CourseEindcase.Repositories;

public class CourseImportRepository : ICoursesImportRepository
{
    private readonly CaseContext _caseContext;
    
    public CourseImportRepository(CaseContext caseContext)
    {
        _caseContext = caseContext;
    }
    
    public async Task AddCourse(Course courseToAdd) {
        _caseContext.Update(courseToAdd);
        await _caseContext.SaveChangesAsync();
    }

    public async Task<Course> GetCourseWithCourseEditions(Course course) => await GetCourseWithCourseEditions(course.CourseCode, course.Title, course.Duration);
    
    private async Task<Course> GetCourseWithCourseEditions(string code, string title, int duration) => await _caseContext.Courses.Include("Editions").FirstOrDefaultAsync(c => c.CourseCode == code && c.Title == title && c.Duration == duration);
    public async Task SaveChanges() => await _caseContext.SaveChangesAsync();
}