using SeaBattle.Domain.AI;

namespace SeaBattle.Domain.GameRules;

/// <summary>
/// Represents the game rule data for a Sea Battle game.
/// </summary>
public class GameRuleData
{
    /// <summary>
    /// Gets the difficulty level of the AI opponent.
    /// </summary>
    public AIDifficultyLevel DifficultyLevel { get; private set; }

    /// <summary>
    /// Gets the width of the game board.
    /// </summary>
    public uint BoardWidth { get; private set; }

    /// <summary>
    /// Gets the height of the game board.
    /// </summary>
    public uint BoardHeight { get; private set; }

    /// <summary>
    /// Gets the sizes of the ships in the game.
    /// </summary>
    public uint[] ShipsSizes { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="GameRuleData"/> class.
    /// </summary>
    /// <param name="difficultyLevel">The difficulty level of the AI opponent.</param>
    /// <param name="boardWidth">The width of the game board.</param>
    /// <param name="boardHeight">The height of the game board.</param>
    /// <param name="shipsSizes">The sizes of the ships in the game.</param>
    public GameRuleData(AIDifficultyLevel difficultyLevel, uint boardWidth, uint boardHeight, uint[] shipsSizes)
    {
        DifficultyLevel = difficultyLevel;
        BoardWidth = boardWidth;
        BoardHeight = boardHeight;
        ShipsSizes = shipsSizes;
    }
}
