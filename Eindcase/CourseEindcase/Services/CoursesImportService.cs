using EindcaseOefenen.DTO;
using EindcaseOefenen.Interfaces;
using EindcaseOefenen.Model;
using EindcaseOefenen.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EindcaseOefenen.Services;

public class CoursesImportService : ICoursesImportService
{
    private readonly ICoursesImportRepository _coursesImportRepository;
    
    public CoursesImportService(ICoursesImportRepository coursesImportRepository)
    {
        _coursesImportRepository = coursesImportRepository;
    }

    public async Task<ActionResult<ImportCoursesReply>> ImportCourses(IEnumerable<Course> courses, IEnumerable<CourseEdition> courseEditions)
    {
        int coursesAdded = 0;
        int courseEditionsAdded = 0;
        foreach (var courseToAdd in courses)
        {
            Course existing = await _coursesImportRepository.GetCoursesWithEditions(courseToAdd);
            CourseEdition existingEdition = await _coursesImportRepository.GetCourseEditions(courseToAdd);
            if (existing is null)
            {
                await _coursesImportRepository.AddCourse(courseToAdd);
                coursesAdded++;
            }
        }
        await _coursesImportRepository.SaveChanges();
        return new ImportCoursesReply() { CoursesAdded = coursesAdded};
    }
}