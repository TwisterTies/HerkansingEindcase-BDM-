using CourseEindcase.DTO;

namespace CourseEindcase.Interfaces;

public interface ICoursesOverviewRepository
{
    Task<IEnumerable<CourseOverview>> GetCoursesOverview();
}