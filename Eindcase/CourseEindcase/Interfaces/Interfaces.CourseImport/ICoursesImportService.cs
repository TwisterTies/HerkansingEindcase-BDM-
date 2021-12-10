using CourseEindcase.DTO;

namespace CourseEindcase.Interfaces;

public interface ICoursesImportService
{
    Task<ImportReply> ImportCourses(IFormFile file);
}