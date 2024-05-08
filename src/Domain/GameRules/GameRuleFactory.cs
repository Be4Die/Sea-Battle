namespace SeaBattle.Domain.GameRules;

public class GameRuleFactory
{
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
