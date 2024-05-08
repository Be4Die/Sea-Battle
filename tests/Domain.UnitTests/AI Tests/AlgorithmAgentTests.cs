namespace Domain.UnitTests.AITests;
using SeaBattle.Domain.GameBoard;

using SeaBattle.Domain.AI;

public class AlgorithmAgentTests
{
    [Fact]
    public void OnHited_Event_Is_Raised_When_MakeMove_Is_Called()
    {
        // Arrange
        var board = new Board(10, 10);
        var agent = new AlgorithmAgent(board);
        bool eventRaised = false;
        board.OnHited += (x, y, result) => eventRaised = true;

        // Act
        agent.MakeMove();

        // Assert
        Assert.True(eventRaised);
    }


    [Fact]
    public void Full_Board_Hited_When_MakeMove_Count_Equels_Board_Size()
    {
        // Arrange
        var board = new Board(10, 10);
        var agent = new AlgorithmAgent(board);
        List<(uint, uint)> hited = new();
        board.OnHited += (x, y, result) => hited.Add((x,y));

        // Act
        for (int i = 0; i < board.Height; i++)
        {
            for (int j = 0; j < board.Width; j++)
            {
                agent.MakeMove();
            }
        }

        // Assert
        Assert.Equal(100, hited.Count);
    }
}