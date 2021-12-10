using System.Collections.Generic;
using System.Threading.Tasks;
using CourseEindcase.Controllers;
using CourseEindcase.DTO;
using CourseEindcase.Interfaces;
using Moq;
using Xunit;

namespace CourseEindcase.UnitTests.Controllers;

public class CourseOverviewControllerTests
{
    Mock<ICoursesOverviewService> coursesOverviewServiceMock;
    
    public CourseOverviewControllerTests()
    {
        coursesOverviewServiceMock = new Mock<ICoursesOverviewService>();
    }
    
    [Fact]
    public async Task GetCoursesOverview_ReturnsCoursesOverview()
    {
        // Arrange
        var coursesOverview = new List<CourseOverview>();
        coursesOverviewServiceMock.Setup(x => x.GetCoursesOverview()).ReturnsAsync(coursesOverview);
        var controller = new CourseOverviewController(coursesOverviewServiceMock.Object);

        // Act
        var result = await controller.GetCoursesOverview();

        // Assert
        Assert.Equal(coursesOverview, result);
    }
}