using CourseEindcase.DTO;
using CourseEindcase.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CourseEindcase.Data;

public class CaseContext : DbContext
{
    public CaseContext(DbContextOptions<CaseContext> options) : base(options)
    {
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseEdition> CourseEditions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(ConfigureCourse);
    }
    
    private void ConfigureCourse(EntityTypeBuilder<Course> builder) {
        builder.Property(p => p.Title)
            .IsRequired(true)
            .HasMaxLength(300);
        builder.Property(p => p.CourseCode)
            .IsRequired(true)
            .HasMaxLength(10);
    }
}