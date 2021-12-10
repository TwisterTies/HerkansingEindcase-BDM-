using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CourseEindcase.Parsers.CoursePropertyParsers;

public class CourseTitleParser : ICoursePropertyParser<string>
{
    private readonly Regex _regex;

    public CourseTitleParser()
    {
        _regex =  new Regex(@"(?<=Titel:\s).*");
    }
    
    public string Parse(string input)
    {
        Match match = _regex.Match(input);
        if (!match.Success)
            throw new ValidationException("Titel niet gevonden");
        return match.Value;
    }
}