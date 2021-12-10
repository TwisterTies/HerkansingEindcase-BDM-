using CourseEindcase.DTO;
using CourseEindcase.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CourseEindcase.Services;

public class CourseOverviewService : ICoursesOverviewService
{
    private readonly ICoursesOverviewRepository _coursesOverviewRepository;
    
    public CourseOverviewService(ICoursesOverviewRepository coursesOverviewRepository)
    {
        _coursesOverviewRepository = coursesOverviewRepository;
    }
    
    public Task<IEnumerable<CourseOverview>> GetCoursesOverview() => _coursesOverviewRepository.GetCoursesOverview();
}