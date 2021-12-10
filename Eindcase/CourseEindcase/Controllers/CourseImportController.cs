using CourseEindcase.DTO;
using CourseEindcase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseEindcase.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseImportController : Controller
{
    private readonly ICoursesImportService _coursesImportService;
    
    public CourseImportController(ICoursesImportService coursesImportService)
    {
        _coursesImportService = coursesImportService;
    }

    [HttpPost]
    public async Task<ImportReply> ImportFile(IFormFile file)
    {
        return await _coursesImportService.ImportCourses(file);
    }
}