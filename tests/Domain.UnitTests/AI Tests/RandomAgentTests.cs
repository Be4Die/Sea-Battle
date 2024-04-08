namespace Domain.UnitTests.AI_Tests;
using SeaBattle.Domain.AI;

public class RandomAgentTests 
{
    [Fact]
    public void OnHited_Event_Is_Raised_When_MakeMove_Is_Called()
    {
        // Arrange
        var board = new Board(10, 10);
        var agent = new RandomAgent(board);
        bool eventRaised = false;
        board.OnHited += (x, y, result) => eventRaised = true;

        // Act
        agent.MakeMove();

        // Assert
        Assert.True(eventRaised);
    }
}
