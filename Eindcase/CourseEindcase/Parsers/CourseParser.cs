using System.ComponentModel.DataAnnotations;
using CourseEindcase.Model;

namespace CourseEindcase.Parsers;

public class CourseParser : ICourseParser
{
    private readonly ICoursePropertyParser<string> _titleParser;
    private readonly ICoursePropertyParser<string> _codeParser;
    private readonly ICoursePropertyParser<int> _durationParser;
    private readonly ICoursePropertyParser<DateTime> _startDateParser;
    private readonly ICoursePropertyParser<string> _emptyParser;
    
    public CourseParser(ICoursePropertyParser<string> titleParser, ICoursePropertyParser<string> codeParser, ICoursePropertyParser<int> durationParser, ICoursePropertyParser<DateTime> startDateParser, ICoursePropertyParser<string> emptyParser)
    {
        _titleParser = titleParser;
        _codeParser = codeParser;
        _durationParser = durationParser;
        _startDateParser = startDateParser;
        _emptyParser = emptyParser;
    }

    public List<Course> Parse(string content)
    {
        List<Course> courses = new List<Course>();
        int fileLines = 0;
        string[] lines = content.Split('\n');

        try
        {
            while (fileLines < lines.Length - 1)
            {
                string titleString = lines[fileLines];
                string title = _titleParser.Parse(titleString);
                fileLines++;

                string courseCodeString = lines[fileLines];
                string courseCode = _codeParser.Parse(courseCodeString);
                fileLines++;

                string durationString = lines[fileLines];
                int duration = _durationParser.Parse(durationString);
                fileLines++;

                string startDateString = lines[fileLines];
                DateTime startDate = _startDateParser.Parse(startDateString);
                fileLines += 2;

                Course course = new Course()
                    { Title = title, Duration = duration, CourseCode = courseCode, Editions = new List<CourseEdition>() };
                CourseEdition edition = new CourseEdition()
                    { StartDatum = startDate };
                Course existingCourse = courses.FirstOrDefault(c =>
                    c.Title == course.Title && c.CourseCode == course.CourseCode && c.Duration == c.Duration);
                if (existingCourse == null)
                {
                    course.Editions.Add(edition);
                    courses.Add(course);
                }
                else
                {
                    if (!existingCourse.Editions.Any(e => e.StartDatum == edition.StartDatum))
                        existingCourse.Editions.Add(edition);
                }
            }
        }
        catch (ValidationException e)
        {
            Console.WriteLine(e.Message);
        }
        return courses;
    }
}

