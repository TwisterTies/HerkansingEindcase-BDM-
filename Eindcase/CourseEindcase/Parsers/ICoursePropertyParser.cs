namespace CourseEindcase.Parsers;

public interface ICoursePropertyParser<T>
{
    T Parse(string text);
}