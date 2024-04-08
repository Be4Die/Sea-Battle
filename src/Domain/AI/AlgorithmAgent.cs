namespace SeaBattle.Domain.AI;

/// <summary>
/// Represents an AI agent that uses a specific algorithm to play the game.
/// This agent maintains a data grid to track the state of the game board and
/// uses a predefined pattern to make moves when the board size is 10x10.
/// </summary>
public class AlgorithmAgent : IAIAgent
{
    protected readonly Board _board;
    protected readonly Random _random = new Random();
    protected readonly (uint, uint)[] _pattern = new (uint, uint)[]{ 
        (2,0), (6,0), (3,1), (7,1), (1,2), (5, 2), 
        (9,2), (0,3), (4,3), (8,3), (2,4), (6,4),
        (3,5), (7,5), (1,6), (5,6), (9,6), (0,7),
        (4,7), (9,7), (2,8), (6,8), (3,9), (7,9)};


    // -1 - not data
    // 0 - checked empty ceil
    // 1 - checked ceil with ship
    // 2 - unchecked, but garanted empty ceil
    protected int[,] _data;

    protected int _currentPatternMoveIndex = 0;

    public AlgorithmAgent(Board board)
    {
        _board = board;
        _data = new int[_board.Width, _board.Height];
        for (int x = 0; x < _board.Width; x++)
        {
            for (int y = 0; y < _board.Height; y++)
            {
                _data[x, y] = -1;
            }
        }
    }

    // This method makes a move on the board
    public virtual void MakeMove()
    {
        (uint, uint) point;

        // If the board size is 10x10, use the pattern to make a move
        if (_board.Height == 10 && _board.Width == 10)
        {
            var tpoint = ChoiceCeilByPattern();
            point = tpoint == (-1, -1) ? ChoiceCeilWhenDeadEnd() : ((uint)tpoint.Item1, (uint)tpoint.Item2);
        }
        else 
            point = ChoiceCeilWhenDeadEnd();

        var res = _board.Hit(point.Item1, point.Item2);
        _data[point.Item1, point.Item2] = res ? 1 : 0;
        UpdateData();
    }

    #region Data Update
    // This method updates the data based on the board state
    protected virtual void UpdateData()
    {
        for (int i = 0; i < _data.GetLength(0); i++)
        {
            for (int j = 0; j < _data.GetLength(1); j++)
            {
                if (_data[i, j] == 0 || _data[i, j] == 2)
                {
                    List<(int, int)> vector = FindVector(i, j);
                    if (vector.Count >= 3)
                    {
                        MarkCellsAroundVector(vector);
                    }
                }
            }
        }
    }

    // This method finds a vector of empty cells starting from the given cell

    private List<(int, int)> FindVector(int x, int y)
    {
        List<(int, int)> vector = new List<(int, int)>();
        vector.Add((x, y));

        // check up
        int i = x - 1;
        while (i >= 0 && (_data[i, y] == 0 || _data[i, y] == 2))
        {
            vector.Add((i, y));
            i--;
        }

        // check down
        i = x + 1;
        while (i < _data.GetLength(0) && (_data[i, y] == 0 || _data[i, y] == 2))
        {
            vector.Add((i, y));
            i++;
        }

        // check left
        int j = y - 1;
        while (j >= 0 && (_data[x, j] == 0 || _data[x, j] == 2))
        {
            vector.Add((x, j));
            j--;
        }

        // check right
        j = y + 1;
        while (j < _data.GetLength(1) && (_data[x, j] == 0 || _data[x, j] == 2))
        {
            vector.Add((x, j));
            j++;
        }

        return vector;
    }

    // This method marks the cells around the given vector as guaranteed empty
    private void MarkCellsAroundVector(List<(int, int)> vector)
    {
        foreach (var cell in vector)
        {
            int x = cell.Item1;
            int y = cell.Item2;

            // Проверяем соседние клетки
            for (int i = x - 1; i <= x + 1; i++)
            {
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (i >= 0 && i < _data.GetLength(0) && j >= 0 && j < _data.GetLength(1) && _data[i, j] == -1)
                    {
                        _data[i, j] = 2;
                    }
                }
            }
        }
    }
    #endregion

    // This method chooses a random cell when there are no more moves in the pattern
    protected virtual (uint, uint) ChoiceCeilWhenDeadEnd() 
    {
        List<(uint, uint)> correctCells = new List<(uint, uint)>();

        for (uint i = 0; i < _data.GetLength(0); i++)
        {
            for (uint j = 0; j < _data.GetLength(1); j++)
            {
                if (_data[i, j] == -1)
                {
                    correctCells.Add((i, j));
                }
            }
        }


        if (correctCells.Count > 0)
        {
            int index = _random.Next(correctCells.Count);
            return correctCells[index];
        }

        return (0, 0);
    }

    // This method chooses a cell from the pattern
    protected virtual (int, int) ChoiceCeilByPattern()
    {
        if (_currentPatternMoveIndex >= _pattern.Length)
            return (-1, -1);
        var move = _pattern[_currentPatternMoveIndex];
        _currentPatternMoveIndex++;
        return ((int)move.Item1, (int)move.Item1);
    }
}
