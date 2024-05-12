using System.Text;

namespace SeaBattle.Domain.GameBoard;

/// <summary>
/// Represents the game board for the Sea Battle game.
/// The board manages the placement and removal of ships, as well as handling hits.
/// </summary>
public class Board : IDisposable
{
    /// <summary>
    /// Event triggered when a ship is placed on the board.
    /// </summary>
    public event Action<Ship, uint, uint>? OnShipPlaced;

    /// <summary>
    /// Event triggered when a ship is removed from the board.
    /// </summary
    public event Action<Ship, uint, uint>? OnShipRemoved;

    /// <summary>
    /// Event triggered when a cell on the board is hit.
    /// </summary>
    public event Action<uint, uint, bool>? OnHited;


    /// <summary>
    /// Gets the width of the game board.
    /// </summary>
    public uint Width { get; protected set; }

    /// <summary>
    /// Gets the height of the game board.
    /// </summary>
    public uint Height { get; protected set; }

    private Ceil[,] _ceils;


    /// <summary>
    /// Initializes a new instance of the Board class with the specified width and height.
    /// </summary>
    /// <param name="width">The width of the board.</param>
    /// <param name="height">The height of the board.</param>
    public Board(uint width, uint height)
    {
        Width = width; 
        Height = height;
        _ceils = new Ceil[width, height];

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                _ceils[i, j] = new();
    }

    /// <summary>
    /// Initializes a new instance of the Board class with the specified array of cells.
    /// </summary>
    /// <param name="ceils">The array of cells representing the board.</param>
    public Board(Ceil[,] ceils)
    {
        Width = (uint)ceils.GetLength(0);
        Height = (uint)ceils.GetLength(1);
        _ceils = ceils;
    }


    /// <summary>
    /// Clears the board, removing all ships and resetting all cells.
    /// </summary>
    public void Clear()
    {
        _ceils = new Ceil[Width, Height];

        for (int i = 0; i < Width; i++)
            for (int j = 0; j < Height; j++)
                _ceils[i, j] = new();
    }

    /// <summary>
    /// Checks if all ships on the board are destroyed.
    /// </summary>
    /// <returns>True if all ships are destroyed; otherwise, false.</returns>
    public bool CheckAllShipsDestroiedCondition()
    {
        bool isDestroied = true;
        for (int i = 0; i < _ceils.GetLength(0); i++)
        {
            for (int j = 0; j < _ceils.GetLength(1); j++)
            {
                if (_ceils[i, j].ContainShip == true && _ceils[i,j].Ship?.IsAlive == true)
                {
                    return false;
                }
            }
        }
        return isDestroied;
    }

    /// <summary>
    /// Attempts to hit a cell at the specified coordinates.
    /// </summary>
    /// <param name="x">The X coordinate of the cell to hit.</param>
    /// <param name="y">The Y coordinate of the cell to hit.</param>
    /// <returns>True if the cell was hit; otherwise, false.</returns>
    public bool Hit(uint x, uint y)
    {
        if (x >= _ceils.GetLength(0) || y >= _ceils.GetLength(1))
            return false;

        var result = _ceils[x, y].Hit();
        OnHited?.Invoke(x, y, result);
        return result;
    }

    /// <summary>
    /// Removes a ship from the board starting at the specified coordinates.
    /// </summary>
    /// <param name="ship">The ship to remove.</param>
    /// <param name="xStart">The starting X coordinate of the ship.</param>
    /// <param name="yStart">The starting Y coordinate of the ship.</param>
    /// <returns>True if the ship was successfully removed; otherwise, false.</returns>
    public bool RemoveShip(Ship ship, uint xStart, uint yStart)
    {
        if (_ceils[xStart, yStart].Ship != ship)
            return false;

        for (uint i = 0; i < ship.Length; i++)
        {
            uint x = xStart;
            uint y = yStart;

            if (ship.Orientation == Ship.Orientations.Horizontal)
            {
                x += i;
            }
            else if (ship.Orientation == Ship.Orientations.Vertical)
            {
                y += i;
            }

            _ceils[x, y].RemoveShipSegment();
        }


        OnShipRemoved?.Invoke(ship, xStart, yStart);
        return true;
    }

    /// <summary>
    /// Places a ship on the board starting at the specified coordinates.
    /// </summary>
    /// <param name="ship">The ship to place.</param>
    /// <param name="xStart">The starting X coordinate of the ship.</param>
    /// <param name="yStart">The starting Y coordinate of the ship.</param>
    /// <returns>True if the ship was successfully placed; otherwise, false.</returns>
    public bool PlaceShip(Ship ship, uint xStart, uint yStart)
    {
        if (!CanShipPlaced(ship, xStart, yStart))
            return false;

        for (uint i = 0; i < ship.Length; i++)
        {
            uint x = xStart;
            uint y = yStart;

            if (ship.Orientation == Ship.Orientations.Horizontal)
            {
                x += i;
            }
            else if (ship.Orientation == Ship.Orientations.Vertical)
            {
                y += i;
            }

            _ceils[x, y].PlaceShipSegment(ship, (int)i);
        }
        OnShipPlaced?.Invoke(ship, xStart, yStart);
        return true;
    }

    /// <summary>
    /// Gets the cell at the specified coordinates.
    /// </summary>
    /// <param name="x">The X coordinate of the cell.</param>
    /// <param name="y">The Y coordinate of the cell.</param>
    /// <returns>The cell at the specified coordinates, or null if the coordinates are out of bounds.</returns>
    public Ceil? GetCeil(uint x, uint y) 
    {
        if (x >= _ceils.GetLength(0) || y >= _ceils.GetLength(1))
            return null;

        return _ceils[x, y];
    }

    /// <summary>
    /// Determines if a ship can be placed at the specified coordinates.
    /// </summary>
    /// <param name="ship">The ship to check for placement.</param>
    /// <param name="xStart">The starting X coordinate of the ship.</param>
    /// <param name="yStart">The starting Y coordinate of the ship.</param>
    /// <returns>True if the ship can be placed; otherwise, false.</returns>
    public bool CanShipPlaced(Ship ship, uint xStart, uint yStart)
    {
        if (xStart >= _ceils.GetLength(0) || yStart >= _ceils.GetLength(1))
            return false;

        if (ship.Orientation == Ship.Orientations.Horizontal && xStart + ship.Length > _ceils.GetLength(0))
            return false;
        else if (ship.Orientation == Ship.Orientations.Vertical && yStart + ship.Length > _ceils.GetLength(1))
            return false;

        for (uint i = 0; i < ship.Length; i++)
        {
            uint x = xStart;
            uint y = yStart;

            if (ship.Orientation == Ship.Orientations.Horizontal)
            {
                x += i;
            }
            else if (ship.Orientation == Ship.Orientations.Vertical)
            {
                y += i;
            }

            if (_ceils[x, y].ContainShip)
            {
                return false;
            }

            // Проверяем соседние клетки
            if (x > 0 && _ceils[x - 1, y].ContainShip)
            {
                return false;
            }
            if (x < _ceils.GetLength(0) - 1 && _ceils[x + 1, y].ContainShip)
            {
                return false;
            }
            if (y > 0 && _ceils[x, y - 1].ContainShip)
            {
                return false;
            }
            if (y < _ceils.GetLength(1) - 1 && _ceils[x, y + 1].ContainShip)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Releases unmanaged resources used by the object.
    /// </summary>
    /// <remarks>
    /// This method sets all event delegates to null to prevent memory leaks.
    /// </remarks>
    public void Dispose()
    {
        OnHited = null;
        OnShipPlaced = null;
        OnShipRemoved = null;
        GC.SuppressFinalize(this);
    }


    /// <summary>
    /// Returns a string that represents the current object.
    /// </summary>
    /// <returns>A string that represents the state of the game board.</returns>
    /// <remarks>
    /// This method creates a string that represents the state of the game board.
    /// Each cell is represented by a character:
    /// '0' - an empty cell, not hit.
    /// '1' - a cell with a ship, not hit.
    /// '2' - a cell with a ship, hit.
    /// '-1' - an empty cell, hit.
    /// </remarks>
    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append("Board: ");
        sb.AppendLine();
        for (int i = 0; i < Width; i++)
        {   
            for (int j = 0; j < Height; j++)
            {
                if (_ceils[i, j].ContainShip)
                {
                    if (_ceils[i,j].IsHited)
                        sb.Append('2');
                    else
                        sb.Append('1');
                }
                else
                {
                    if (_ceils[i, j].IsHited)
                        sb.Append("-1");
                    else
                        sb.Append('0');
                }
                sb.Append(' ');
            }
            sb.AppendLine();
        }
        sb.AppendLine();

        return sb.ToString();
    }
}
