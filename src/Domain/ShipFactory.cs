using SeaBattle.Domain.GameRules;

namespace SeaBattle.Domain;

public class ShipFactory
{

    public Ship[] CreateShipsFromGameRule(GameRuleData gameRule) => Enumerable.Range(0, gameRule.ShipsSizes.Length)
            .Select(i => new Ship(Ship.Orientations.Vertical, gameRule.ShipsSizes[i]))
            .ToArray();
}
