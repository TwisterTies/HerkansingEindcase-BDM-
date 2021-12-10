using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseEindcase.DTO;
using CourseEindcase.Interfaces;
using CourseEindcase.Services;
using Moq;
using Xunit;

namespace CourseEindcase.UnitTests.Services;

public class CourseOverviewServiceTest
{
    
    private Mock<ICoursesOverviewRepository> _mockCourseOverviewRepository;
    private CourseOverviewService _courseOverviewService;
    
    public CourseOverviewServiceTest()
    {
        _mockCourseOverviewRepository = new Mock<ICoursesOverviewRepository>();
        _courseOverviewService = new CourseOverviewService(_mockCourseOverviewRepository.Object);
    }
    
    [Fact]
    public async Task GetCourseOverview_ReturnsCourseOverview()
    {
        // Arrange
        List<CourseOverview> courseOverview = new List<CourseOverview>();
        courseOverview.Add(new CourseOverview()
        {
            Title = "Test",
            Duration = 5,
            StartDate = new DateTime(2020, 1, 1),
            EditionId = 2
        });
        
        _mockCourseOverviewRepository.Setup(x => x.GetCoursesOverview()).ReturnsAsync(courseOverview); 
        IEnumerable<CourseOverview> actualCourseOverviews = await  _courseOverviewService.GetCoursesOverview();
        
        // Act

        // Assert
        Assert.Equal(courseOverview, actualCourseOverviews);
    }
}