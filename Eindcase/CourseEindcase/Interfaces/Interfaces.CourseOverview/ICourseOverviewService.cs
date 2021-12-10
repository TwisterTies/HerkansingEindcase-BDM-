using CourseEindcase.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CourseEindcase.Interfaces;

public interface ICoursesOverviewService {
    Task<IEnumerable<CourseOverview>> GetCoursesOverview();
}