using SeaBattle.Domain.GameBoard;

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


    public void FillBoard(Board board, Ship[] shipsToPlace)
    {

        // Create a list of all available coordinates
        var availableCoordinates = new List<(int, int)>();
        for (int x = 0; x < board.Width; x++)
        {
            for (int y = 0; y < board.Height; y++)
            {
                availableCoordinates.Add((x, y));
            }
        }

        foreach (var ship in shipsToPlace)
        {
            bool isPlaced = false;

            while (!isPlaced && availableCoordinates.Count > 0)
            {
                // We choose a random coordinate from the available ones
                int index = _random.Next(availableCoordinates.Count);
                var (xStart, yStart) = availableCoordinates[index];
                availableCoordinates.RemoveAt(index); // Remove the selected coordinate from the available coordinates

                // Attempting to place the ship at the selected coordinates
                isPlaced = board.PlaceShip(ship, (uint)xStart, (uint)yStart);
            }
        }
    }

    public Board GenerateBoard(uint width, uint height, Ship[] shipsToPlace)
    {
        var board = new Board(width, height);

        // Create a list of all available coordinates
        var availableCoordinates = new List<(int, int)>();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                availableCoordinates.Add((x, y));
            }
        }

        foreach (var ship in shipsToPlace)
        {
            bool isPlaced = false;

            while (!isPlaced && availableCoordinates.Count > 0)
            {
                // We choose a random coordinate from the available ones
                int index = _random.Next(availableCoordinates.Count);
                var (xStart, yStart) = availableCoordinates[index];
                availableCoordinates.RemoveAt(index); // Remove the selected coordinate from the available coordinates

                // Attempting to place the ship at the selected coordinates
                isPlaced = board.PlaceShip(ship, (uint)xStart, (uint)yStart);
            }
        }

        return board;
    }
    #region Try to crerate Genetic Algorithm
    /*
    private List<int> GeneticAlgorithm(uint width, uint height, Ship[] shipsToPlace)
    {
        var shipsCount = shipsToPlace.Length;
        var lengthChrome = 3 * shipsCount;
        int populationSize = 10;
        float pCrossover = 0.9f;
        float pMutation = 0.2f;
        int maxGenerations = 50;
        int hallOfFameSize = 1;

        // Генерируем начальную популяцию
        var population = Enumerable.Range(0, populationSize)
            .Select(_ => RandomIndividual(lengthChrome, width, height))
            .ToList();

        // Оценка популяции
        var fitness = population.Select(individual => ShipsFitness(individual, width, height, shipsToPlace)).ToList();

        // Основной цикл генетического алгоритма
        for (int gen = 0; gen < maxGenerations; gen++)
        {
            // Селекция
            var selected = population.Zip(fitness, (ind, fit) => (ind, fit))
                .OrderByDescending(x => x.fit)
                .Take(hallOfFameSize)
                .Select(x => x.ind)
                .ToList();

            // Кроссовер
            var offspring = selected.SelectMany(ind =>
                selected.Where(ind2 => ind != ind2).Select(ind2 => Crossover(ind, ind2, pCrossover, width, height))).ToList();

            // Мутация
            offspring = offspring.Select(ind => MutShip(ind, width, height, pMutation)).ToList();

            // Объединение популяций
            population = selected.Concat(offspring).ToList();
            fitness = population.Select(ind => ShipsFitness(ind, width, height, shipsToPlace)).ToList();
        }

        // Выбираем лучших индивидов
        var best = population.Zip(fitness, (ind, fit) => (ind, fit))
            .OrderByDescending(x => x.fit)
            .First().ind;

        return best;
    }

    private List<int> Crossover(List<int> parent1, List<int> parent2, float pCrossover, uint width, uint height)
    {
        if (_random.NextDouble() >= pCrossover)
        {
            // Если не происходит кроссовер, то возвращаем копию родителя
            return new List<int>(parent1);
        }

        // Выбираем случайную точку для разделения
        int cxPoint = _random.Next(parent1.Count);

        // Создаем потомка путем объединения частей родителей
        var child = new List<int>(parent1.Take(cxPoint).Concat(parent2.Skip(cxPoint)));

        // Мутация потомка
        child = MutShip(child, width, height, 0.1f);

        return child;
    }

    private List<int> RandomIndividual(int lenght, uint width, uint height)
    {
        var ind = new List<int>(lenght);
        for (int i = 0; i < lenght; i++)
        {
            ind[i] = (i + 1) % 3 == 0 ? _random.Next(0, 2) :
                    ((i + 1) % 2 == 0 ? _random.Next(0, (int)width) : _random.Next(0, (int)height));
        }

        return ind;
    }

    private List<int> MutShip(List<int> individual, uint width, uint height, float indpb)
    {
        var copy = new List<int>(individual);
        for (int i = 0; i < individual.Count; i++)
        {
            if (_random.NextDouble() < indpb)
                copy[i] = (i + 1) % 3 == 0 ? _random.Next(0, 2) : 
                    ((i + 1) % 2 == 0 ? _random.Next(0, (int)width) : _random.Next(0, (int)height));
        }

        return copy;
    }

    private double ShipsFitness(List<int> individual, uint width, uint height, Ship[] shipsToPlace)
    {
        const int outBoardFine = 1000;
        const float nearShipFine = 0.2f;
        var typeShip = shipsToPlace
            .Select(ship => (int)ship.Length)
            .OrderByDescending(length => length)
            .ToArray(); ;
        var P = new double[width + 6, height + 6]; // Создаем массив с дополнительными границами
        for (int i = 1; i <= width; i++)
        {
            for (int j = 1; j <= height; j++)
            {
                P[i, j] = 0; // Заполняем игровое поле нулями
            }
        }

        for (var i = 0; i < individual.Count; i += 3)
        {
            var ship = individual.GetRange(i, 3);
            var t = typeShip[i / 3];
            int x = ship[0] + 1; // Корректируем индексы для оригинального поля
            int y = ship[1] + 1;

            if (ship[2] == 0) // Горизонтальное расположение
            {
                for (int j = 0; j < t + 2; j++)
                {
                    P[x, y + j] += nearShipFine;
                    if (j > 0 && j < t + 1)
                        P[x, y + j] = 1;
                }
            }
            else // Вертикальное расположение
            {
                for (int j = 0; j < t + 2; j++)
                {
                    P[x + j, y] += nearShipFine;
                    if (j > 0 && j < t + 1)
                        P[x + j, y] = 1;
                }
            }
        }

        double s = 0;
        for (int i = 0; i < width + 6; i++)
        {
            for (int j = 0; j < height + 6; j++)
            {
                if (P[i, j] > 1 && P[i, j] < outBoardFine) // Считаем штрафы в пределах поля
                {
                    s += P[i, j];
                }
                else if (P[i, j] > outBoardFine + nearShipFine * 4) // Штраф за выход за пределы поля
                {
                    s += P[i, j];
                }
            }
        }

        return s;
    }
    */
    #endregion
}
