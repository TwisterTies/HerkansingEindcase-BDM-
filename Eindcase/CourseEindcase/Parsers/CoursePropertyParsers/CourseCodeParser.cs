using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CourseEindcase.Parsers.CoursePropertyParsers;

public class CourseCodeParser : ICoursePropertyParser<string>
{
    private readonly Regex _regex;

    public CourseCodeParser()
    {
        _regex = new Regex(@"(?<=Cursuscode:\s).*");
    }

    public string Parse(string text)
    {
        Match match = _regex.Match(text);
        if (!match.Success)
            throw new ValidationException("Cursuscode niet gevonden");
        return match.Value;
    }
}