namespace CourseEindcase.DTO;

public class ImportReply
{
    public int CoursesAdded { get; set; }
    public int EditionsAdded { get; set; }
    public int DuplicateEditions { get; set; }
    public int DuplicateCourses { get; set; }
    
    public string ErrorMessage { get; set; }
}