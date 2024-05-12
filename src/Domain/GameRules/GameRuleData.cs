using SeaBattle.Domain.AI;

namespace SeaBattle.Domain.GameRules;

/// <summary>
/// Represents the game rule data for a Sea Battle game.
/// </summary>
public class GameRuleData(AIDifficultyLevel difficultyLevel, uint boardWidth, uint boardHeight, uint[] shipsSizes)
{
    /// <summary>
    /// Gets the difficulty level of the AI opponent.
    /// </summary>
    public AIDifficultyLevel DifficultyLevel { get; private set; } = difficultyLevel;

    /// <summary>
    /// Gets the width of the game board.
    /// </summary>
    public uint BoardWidth { get; private set; } = boardWidth;

    /// <summary>
    /// Gets the height of the game board.
    /// </summary>
    public uint BoardHeight { get; private set; } = boardHeight;

    /// <summary>
    /// Gets the sizes of the ships in the game.
    /// </summary>
    public uint[] ShipsSizes { get; private set; } = shipsSizes;
}
