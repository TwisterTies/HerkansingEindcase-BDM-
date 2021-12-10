using System.ComponentModel.DataAnnotations;

namespace CourseEindcase.Model;

public class Course
{
    [Key]
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string CourseCode { get; set; }
    
    public int Duration { get; set; }
    
    public virtual ICollection<CourseEdition> Editions { get; set; }
}