using CourseEindcase.Data;
using CourseEindcase.DTO;
using CourseEindcase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseEindcase.Repositories;

public class CourseOverviewRepository : ICoursesOverviewRepository
{
    private readonly CaseContext _context;
    
    public CourseOverviewRepository(CaseContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<CourseOverview>> GetCoursesOverview()
    {
        return await (from c in _context.Courses
            from e in c.Editions
            where c.Id == e.CourseId
            orderby e.StartDatum, c.Title
            select new CourseOverview
                { StartDate = (e.StartDatum), Duration = c.Duration, Title = c.Title, EditionId = e.Id }).ToListAsync();
    }
}