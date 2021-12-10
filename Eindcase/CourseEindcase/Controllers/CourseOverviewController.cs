using CourseEindcase.DTO;
using CourseEindcase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseEindcase.Controllers;
[Route("/api/[Controller]")]
[ApiController]
public class CourseOverviewController : ControllerBase
{
    private readonly ICoursesOverviewService _coursesOverviewService;

    public CourseOverviewController(ICoursesOverviewService coursesOverviewService) {
        this._coursesOverviewService = coursesOverviewService;
    }
    
    [HttpGet]
    public async Task<IEnumerable<CourseOverview>> GetCoursesOverview()
    {
        return await _coursesOverviewService.GetCoursesOverview();
    }
}