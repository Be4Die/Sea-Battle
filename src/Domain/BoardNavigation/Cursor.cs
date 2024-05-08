using SeaBattle.Domain.GameBoard;

namespace SeaBattle.Domain.BoardNavigation;

public class Cursor
{
    public int X { get; set; }
    public int Y { get; set; }
    public Ceil? ChoosenElement { get; set; }
}
