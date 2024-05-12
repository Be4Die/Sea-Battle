using SeaBattle.Domain.GameBoard;

namespace SeaBattle.Domain.BoardNavigation;

/// <summary>
/// Represents a cursor on the game board.
/// The cursor has X and Y coordinates and can select an element on the <seealso cref="Board"/>.
/// </summary>
public class Cursor
{
    /// <summary>
    /// The X coordinate of the cursor on the game board.
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// The Y coordinate of the cursor on the game board.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// The selected element on the game board.
    /// This can be null if the cursor has not selected an element.
    /// </summary>
    public Ceil? ChoosenElement { get; set; }
}