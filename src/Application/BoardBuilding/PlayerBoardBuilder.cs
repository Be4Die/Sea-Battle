using SeaBattle.Domain.BoardNavigation;
using SeaBattle.Domain;
using static SeaBattle.Domain.Ship;
using SeaBattle.Domain.GameBoard;

namespace SeaBattle.Application.BoardBuilding;

/// <summary>
/// Class responsible for building the player's game board.
/// </summary>
public sealed class PlayerBoardBuilder
{
    public event Action<Board>? OnBuildCompleted;
    public event Action<MoveDirection>? OnMoved;
    public event Action? OnRotated;
    public event Action<(uint, uint)>? OnPlaced;
    public event Action<bool>? OnCanPressChanged;
    public event Action<Ship?>? OnShipChanged;
    public CursorData CursorData { get; private set; } = new();
    public readonly Board Board;

    private readonly Ship[] _orderedShips;
    private int _currentShipIndex;

    /// <summary>
    /// Constructor for the PlayerBoardBuilder class.
    /// </summary>
    /// <param name="orderedShips">Array of ships to be placed on the game board.</param>
    /// <param name="board">Board whre ships placed.</param>
    public PlayerBoardBuilder(Board board, Ship[] orderedShips)
    {
        _orderedShips = orderedShips;
        _currentShipIndex = 0;
        CursorData.HoldingShip = _orderedShips[0];
        Board = board;
    }

    public void Restart()
    {
        _currentShipIndex = 0;
        CursorData.HoldingShip = _orderedShips[0];
        foreach (var item in _orderedShips)
        {
            item.SetShipAlive();
        }
    }

    /// <summary>
    /// Moves the ship in the specified direction.
    /// </summary>
    /// <param name="direction">Direction of movement.</param>
    public void MoveShip(MoveDirection direction)
    {
        if (CursorData.HoldingShip == null)
            return;

        switch (direction)
        {
            case MoveDirection.Left: CursorData.X = (int)Math.Clamp(CursorData.X - 1, 0, Board.Width-1); break;
            case MoveDirection.Right: CursorData.X = (int)Math.Clamp(CursorData.X + 1, 0, Board.Width-1); break;
            case MoveDirection.Up: CursorData.Y = (int)Math.Clamp(CursorData.Y - 1, 0, Board.Height-1); break;
            case MoveDirection.Down: CursorData.Y = (int)Math.Clamp(CursorData.Y + 1, 0, Board.Height-1); break;
            default: throw new NotImplementedException();
        }

        OnMoved?.Invoke(direction);
        OnCanPressChanged?.Invoke(Board.CanShipPlaced(CursorData.HoldingShip, (uint)CursorData.X, (uint)CursorData.Y));
    }

    /// <summary>
    /// Rotates the ship.
    /// </summary>
    public void RotateShip()
    {
        if (CursorData.HoldingShip == null)
            return;

        CursorData.HoldingShip = CursorData.HoldingShip.Orientation == Orientations.Vertical
            ? new Ship(Orientations.Horizontal, CursorData.HoldingShip.Length)
            : new Ship(Orientations.Vertical, CursorData.HoldingShip.Length);

        OnRotated?.Invoke();
        OnShipChanged?.Invoke(CursorData.HoldingShip);
        OnCanPressChanged?.Invoke(Board.CanShipPlaced(CursorData.HoldingShip, (uint)CursorData.X, (uint)CursorData.Y));
    }

    /// <summary>
    /// Places the ship on the game board.
    /// </summary>
    public void PlaceShip()
    {
        if (CursorData.HoldingShip == null)
            return;

        var canPlaced = Board.CanShipPlaced(CursorData.HoldingShip, (uint)CursorData.X, (uint)CursorData.Y);
        OnCanPressChanged?.Invoke(canPlaced);
        if (!canPlaced)
            return;

        Board.PlaceShip(CursorData.HoldingShip, (uint)CursorData.X, (uint)CursorData.Y);
        OnPlaced?.Invoke(((uint)CursorData.X, (uint)CursorData.Y));

        _currentShipIndex++;
        if (_currentShipIndex >= _orderedShips.Length)
        {
            CursorData.HoldingShip = null;
            OnBuildCompleted?.Invoke(Board);
            OnCanPressChanged?.Invoke(false);
        }
        else
        {
            CursorData.HoldingShip = _orderedShips[_currentShipIndex];
        }
        OnShipChanged?.Invoke(CursorData.HoldingShip);
    }
}