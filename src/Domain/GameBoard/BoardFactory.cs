using SeaBattle.Domain.AI;
using SeaBattle.Domain.GameRules;

namespace SeaBattle.Domain.GameBoard;

/// <summary>
/// Factory for creating game <seealso cref="Board"/>.
/// </summary>
public class BoardFactory
{
    /// <summary>
    /// Creates a game board based on the game rules.
    /// </summary>
    /// <param name="data">Game rule data containing the board dimensions.</param>
    /// <returns>A new game board with the specified dimensions.</returns>
    public static Board CreateBoardFromRules(GameRuleData data) => new (data.BoardWidth, data.BoardHeight);

    /// <summary>
    /// Creates an enemy board based on the game rules and an AI agent.
    /// </summary>
    /// <param name="data">Game rule data containing the board dimensions.</param>
    /// <param name="agent">The AI agent that will generate the board.</param>
    /// <returns>A new enemy board with the specified dimensions and ships placed.</returns>
    public static Board CreateEnemyBoard(GameRuleData data, IAIAgent agent) => agent.GenerateBoard(data.BoardWidth, data.BoardHeight,
        ShipFactory.CreateShipsFromGameRule(data));
}
