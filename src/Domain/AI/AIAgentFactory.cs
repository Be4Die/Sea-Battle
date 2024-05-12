using SeaBattle.Domain.GameBoard;

namespace SeaBattle.Domain.AI;

/// <summary>
/// Factory for creating AI agents based on the specified difficulty level.
/// </summary>
public static class AIAgentFactory
{
    /// <summary>
    /// Creates an AI agent of the specified difficulty level.
    /// </summary>
    /// <param name="aIDifficulty">The difficulty level of the AI agent to create.</param>
    /// <param name="board">The board that the AI agent will operate on.</param>
    /// <returns>An instance of <see cref="IAIAgent"/> that corresponds to the specified difficulty level.</returns>
    public static IAIAgent CreateAgent(AIDifficultyLevel aIDifficulty, Board board)
    {
        return aIDifficulty switch
        {
            AIDifficultyLevel.Easy => new RandomAgent(board),
            AIDifficultyLevel.Medium => new RandomAgent(board),
            _ => new RandomAgent(board)
        };
    }
}
