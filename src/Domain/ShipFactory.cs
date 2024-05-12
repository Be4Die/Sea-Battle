using SeaBattle.Domain.GameRules;

namespace SeaBattle.Domain;

/// <summary>
/// Factory for creating ships based on game rule data.
/// </summary>
public class ShipFactory
{
    /// <summary>
    /// Creates an array of ships from the game rule data.
    /// All ships are created with a vertical orientation and their sizes are determined by the game rule data.
    /// </summary>
    /// <param name="gameRule">The game rule data that contains the ship sizes.</param>
    /// <returns>An array of <see cref="Ship"/> objects created from the game rule data.</returns>
    public Ship[] CreateShipsFromGameRule(GameRuleData gameRule) => Enumerable.Range(0, gameRule.ShipsSizes.Length)
            .Select(i => new Ship(Ship.Orientations.Vertical, gameRule.ShipsSizes[i]))
            .ToArray();
}
