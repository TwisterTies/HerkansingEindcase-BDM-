using System.ComponentModel.DataAnnotations;
using CourseEindcase.DTO;
using CourseEindcase.Interfaces;
using CourseEindcase.Model;
using CourseEindcase.Parsers;
using CourseEindcase.Repositories;

namespace CourseEindcase.Services;

public class CourseImportService : ICoursesImportService
{
    private readonly ICoursesImportRepository _courseImportRepository;
    private readonly ICourseParser _courseParser;
    
    public CourseImportService(ICoursesImportRepository courseImportRepository, ICourseParser courseParser)
    {
        _courseImportRepository = courseImportRepository;
        _courseParser = courseParser;
    }

    public async Task<ImportReply> ImportCourses(IFormFile file)
    {
        using StreamReader reader = new StreamReader(file.OpenReadStream());
        var fileContent = await reader.ReadToEndAsync();
        try
        {
            List<Course> courses = _courseParser.Parse(fileContent);
            var coursesAdded = 0;
            var editionsAdded = 0;
            var duplicateEditions = 0;
            var duplicateCourses = 0;
            foreach (var courseToAdd in courses)
            {
                Course existing = await _courseImportRepository.GetCourseWithCourseEditions(courseToAdd);
                if (existing is null)
                {
                    await _courseImportRepository.AddCourse(courseToAdd);
                    coursesAdded++;
                    editionsAdded += courseToAdd.Editions.Count();
                }
                else
                {
                    duplicateCourses++;
                    foreach (CourseEdition courseToAdd_Edition in courseToAdd.Editions)
                    {
                        if (existing.Editions.FirstOrDefault(e => e.StartDatum == courseToAdd_Edition.StartDatum) is
                            null)
                        {
                            existing.Editions.Add(courseToAdd_Edition);
                            editionsAdded++;
                        }
                        else
                        {
                            duplicateEditions++;
                        }
                    }
                }
            }

            await _courseImportRepository.SaveChanges();
            return new ImportReply()
            {
                CoursesAdded = coursesAdded, EditionsAdded = editionsAdded, DuplicateCourses = duplicateCourses,
                DuplicateEditions = duplicateEditions
            };
        }
        catch (ValidationException e)
        {
            string errorMessage = $"{e.Message}";
            return new ImportReply()
            {
                ErrorMessage = errorMessage
            };
        }
    }
}