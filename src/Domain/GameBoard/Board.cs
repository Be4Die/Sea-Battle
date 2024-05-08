namespace SeaBattle.Domain.GameBoard;

public class Board : IDisposable
{
    public event Action<Ship, uint, uint>? OnShipPlaced;
    public event Action<Ship, uint, uint>? OnShipRemoved;
    public event Action<uint, uint, bool>? OnHited;

    public uint Width { get; protected set; }
    public uint Height { get; protected set; }

    private Ceil[,] _ceils;

    public Board(uint width, uint height)
    {
        Width = width; 
        Height = height;
        _ceils = new Ceil[width, height];

        for (int i = 0; i < width; i++)
            for (int j = 0; j < height; j++)
                _ceils[i, j] = new();
    }

    public Board(Ceil[,] ceils)
    {
        Width = (uint)ceils.GetLength(0);
        Height = (uint)ceils.GetLength(1);
        _ceils = ceils;
    }

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

    public bool Hit(uint x, uint y)
    {
        if (x >= _ceils.GetLength(0) || y >= _ceils.GetLength(1))
            return false;

        var result = _ceils[x, y].Hit();
        OnHited?.Invoke(x, y, result);
        return result;
    }

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

    public Ceil? GetCeil(uint x, uint y) 
    {
        if (x >= _ceils.GetLength(0) || y >= _ceils.GetLength(1))
            return null;

        return _ceils[x, y];
    }

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

    public void Dispose()
    {
        OnHited = null;
        OnShipPlaced = null;
        OnShipRemoved = null;
    }
}
