
namespace SeaBattle.Domain.AI;

public static class AIAgentFactory
{
    public static IAIAgent CreateAgent(AIDifficultyLevel aIDifficulty, Board board)
    {
        switch (aIDifficulty)
        {
            case AIDifficultyLevel.Easy: return new RandomAgent(board);
            case AIDifficultyLevel.Medium: return new AlgorithmAgent(board);
            default: return new RandomAgent(board);
        }
    }
}
