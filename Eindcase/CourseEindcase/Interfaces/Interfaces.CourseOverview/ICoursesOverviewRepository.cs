using CourseEindcase.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CourseEindcase.Interfaces;

public interface ICoursesOverviewRepository
{
    Task<IEnumerable<CourseOverview>> GetCoursesOverview();
}