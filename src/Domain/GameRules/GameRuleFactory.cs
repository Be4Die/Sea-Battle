namespace SeaBattle.Domain.GameRules;

/// <summary>
/// Factory for creating game rule data for a Sea Battle game.
/// </summary>
public class GameRuleFactory
{
    /// <summary>
    /// Creates a classic game rule data with easy AI difficulty, 10x10 board size, and standard ship sizes.
    /// </summary>
    /// <returns>A new instance of <see cref="GameRuleData"/> with classic game rule data.</returns>
    public GameRuleData CreateClassicData()
    {
        return new GameRuleData(
            AI.AIDifficultyLevel.Easy, 
            10, 10, 
            new uint[] 
            {
                4, 3, 3, 2, 2, 2, 1, 1, 1, 1
            });
    }
}
