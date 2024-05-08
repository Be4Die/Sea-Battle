using SeaBattle.Domain.AI;

namespace SeaBattle.Domain.GameRules;

public class GameRuleData
{
    public AIDifficultyLevel DifficultyLevel { get; private set; }
    public uint BoardWidth { get; private set; }
    public uint BoardHeight { get; private set; }
    public uint[] ShipsSizes { get; private set; }

    public GameRuleData(AIDifficultyLevel difficultyLevel, uint boardWidth, uint boardHeight, uint[] shipsSizes)
    {
        DifficultyLevel = difficultyLevel;
        BoardWidth = boardWidth;
        BoardHeight = boardHeight;
        ShipsSizes = shipsSizes;
    }
}
