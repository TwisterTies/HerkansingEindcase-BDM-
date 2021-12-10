using CourseEindcase.Model;

namespace CourseEindcase.Parsers;

public interface ICourseParser
{
    List<Course> Parse(string content);
}