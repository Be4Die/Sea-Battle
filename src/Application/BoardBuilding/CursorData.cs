using SeaBattle.Domain;
using SeaBattle.Domain.BoardNavigation;

namespace SeaBattle.Application.BoardBuilding;

public class CursorData : Cursor
{
    public Ship? HoldingShip { get; set; }
    public bool CanPress { get; set; }
}
