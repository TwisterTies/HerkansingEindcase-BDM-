using CourseEindcase.DTO;
using CourseEindcase.Model;
using Microsoft.AspNetCore.Mvc;

namespace CourseEindcase.Repositories;

public interface ICoursesImportRepository
{
    Task<Course> GetCourseWithCourseEditions(Course courseToAdd);
    Task AddCourse(Course courseToAdd);
    Task SaveChanges();
}