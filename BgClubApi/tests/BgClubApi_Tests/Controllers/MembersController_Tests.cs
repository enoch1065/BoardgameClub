using BgClubApi.Controllers;
using BgClubApi.Models;

namespace ControllerTests;

// This class contains unit tests for the controller actions only, using a mock repository
// The intention is to ensure that the controller:
// 1. Calls the correct methods on the repository
// 2. Response includes the data provided by the repository
// 3. Returns correct response types such as NOT_FOUND or BAD_REQUEST
public class MembersController_Tests
{
    [Theory, AutoData]
    public async Task GetMembers_ReturnsFullListOfMembers(IEnumerable<Member> members)
    {
        // Arrange - mock repository will return members list when GetAllMembers is called
        var repositoryMock = new Mock<IMemberRepository>();
        repositoryMock.Setup(r => r.GetAllMembers()).Returns(Task.FromResult(members));
        var controller = new MembersController(repositoryMock.Object);

        // Act
        var controllerActionResult = await controller.GetMembers();

        // Assert
        repositoryMock.Verify(r => r.GetAllMembers());  // Verify that GetAllMembers was called on the mock repo
        Assert.NotNull(controllerActionResult.Result);

        var resultMembers = ((Microsoft.AspNetCore.Mvc.ObjectResult)controllerActionResult.Result).Value as IEnumerable<Member>;
        Assert.NotNull(resultMembers);
        Assert.Equal(members, resultMembers);           // Verify that controller result contains the members list
    }

    [Theory, AutoData]
    public async Task GetMember_ValidId_ReturnsMember(Member member)
    {
        // Arrange - mock repository will return a member when GetMemberById is called
        var repositoryMock = new Mock<IMemberRepository>();
        repositoryMock
            .Setup(r => r.GetMemberById(member.Id))
            .Returns(Task.FromResult((Member?)member));
        var controller = new MembersController(repositoryMock.Object);

        // Act
        var controllerActionResult = await controller.GetMember(member.Id);

        // Assert
        repositoryMock.Verify(r => r.GetMemberById(member.Id)); // Verify that GetMemberById was called on the mock repo
        Assert.NotNull(controllerActionResult.Result);

        var resultMember = ((Microsoft.AspNetCore.Mvc.ObjectResult)controllerActionResult.Result).Value as Member;
        Assert.NotNull(resultMember);
        Assert.Equal(member, resultMember);                     // Verify that controller result contains the member
    }

    [Theory, AutoData]
    public async Task GetMember_BadId_ReturnsNotFound(int id)
    {
        // Arrange - mock repository will return NULL when GetMemberById is called
        var repositoryMock = new Mock<IMemberRepository>();
        repositoryMock
            .Setup(r => r.GetMemberById(id))
            .Returns(Task.FromResult((Member?)null));
        var controller = new MembersController(repositoryMock.Object);

        // Act
        var controllerActionResult = await controller.GetMember(id);

        // Assert
        repositoryMock.Verify(r => r.GetMemberById(id));    // Verify that GetMemberById was called on the mock repo
        Assert.NotNull(controllerActionResult.Result);
        Assert.IsType<Microsoft.AspNetCore.Mvc.NotFoundResult>(controllerActionResult.Result); // Verify that controller result is Not Found
    }
}
