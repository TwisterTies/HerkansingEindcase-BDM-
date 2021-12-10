using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CourseEindcase.Parsers.CoursePropertyParsers;

public class CourseStartdateParser : ICoursePropertyParser<DateTime>
{
    private readonly Regex _regex;

    public CourseStartdateParser()
    {
        _regex = new Regex(@"(?<=Startdatum:\s).*");
    }
    
    public DateTime Parse(string input)
    {
        var match = _regex.Match(input);
        var date = match.Value;
        if (!match.Success)
            throw new ValidationException("Startdatum niet gevonden");
        if (!DateTime.TryParseExact(match.Value, new string[] { "dd/MM/yyyy", "d/MM/yyyy", "dd/M/yyyy", "d/M/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
            throw new ValidationException("Startdatum niet in correct formaat");
        CultureInfo cultureInfo = new CultureInfo("nl-NL");
        return DateTime.Parse(date, cultureInfo);
    }
}