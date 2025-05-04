using BgClubApi.Controllers;
using BgClubApi.Models;

namespace ControllerTests;

public class MembersController_Tests
{
    private static Member member1 = new Member { Id = 1, IsComplete = true, Name = nameof(member1) };
    private static Member member2 = new Member { Id = 2, IsComplete = true, Name = nameof(member2) };
    private static IEnumerable<Member> members = new Member[] { member1, member2 };

    [Fact]
    public void Test1()
    {

    }

    [Fact]
    public async Task GetMembers()
    {
        // Arrange
        var repositoryMock = new Mock<IMemberRepository>();
        repositoryMock
            .Setup(r => r.GetAllMembers())
            .Returns(Task.FromResult(members));

        var controller = new MembersController(repositoryMock.Object);

        // Act
        var controllerActionResult = await controller.GetMembers();

        // Assert
        repositoryMock.Verify(r => r.GetAllMembers());
        Assert.NotNull(controllerActionResult.Result);

        var resultMembers = ((Microsoft.AspNetCore.Mvc.ObjectResult)controllerActionResult.Result).Value as Member[];
        Assert.NotNull(resultMembers);

        Assert.Equal(member1, resultMembers[0]);
        Assert.Equal(member2, resultMembers[1]);
    }
}
