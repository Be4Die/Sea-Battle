using SeaBattle.Domain;
using SeaBattle.Domain.BoardNavigation;

namespace SeaBattle.Application.BoardBuilding;

/// <summary>
/// Represents cursor data used during the board building phase.
/// </summary>
public class CursorData : Cursor
{
    /// <summary>
    /// Gets or sets the ship that the cursor is currently holding.
    /// </summary>
    public Ship? HoldingShip { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the cursor can be pressed.
    /// </summary>
    public bool CanPress { get; set; }
}
