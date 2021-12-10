using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CourseEindcase.Parsers.CoursePropertyParsers;

public class EmptyParser : ICoursePropertyParser<string>
{
    private readonly Regex _regex;
    
    public EmptyParser()
    {
        _regex = new Regex(@"^.{0}$");
    }

    public string Parse(string input)
    {
        var match = _regex.Match(input);
        if (!match.Success)
            throw new ValidationException("Lijn is niet leeg");
        return match.Value;
    }
}