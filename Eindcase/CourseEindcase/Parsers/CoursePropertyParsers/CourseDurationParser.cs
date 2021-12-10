using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CourseEindcase.Parsers.CoursePropertyParsers;

public class CourseDurationParser : ICoursePropertyParser<int>
{
    private readonly Regex _regex;

    public CourseDurationParser()
    {
        _regex = new Regex(@"(?<=Duur:\s).*(?=dagen)");
    }

    public int Parse(string text)
    {
        Match match = _regex.Match(text);
        if (!match.Success)
            throw new ValidationException("Duur niet gevonden");
        if (!int.TryParse(match.Value, out int result))
            throw new ValidationException("Duur niet gevonden");
        return result;
    }
}