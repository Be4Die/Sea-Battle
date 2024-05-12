using SeaBattle.Domain.GameBoard;

namespace SeaBattle.Domain.AI;

/// <summary>
/// Represents an AI agent that makes random moves on the game board.
/// This agent maintains a shuffled list of all possible points on the board
/// and chooses the next move randomly from this list.
/// </summary>
public sealed class RandomAgent : IAIAgent
{
    private readonly Random _random = new Random();
    private readonly Board _board;

    /// <summary>
    /// An array of all possible points on the game board, shuffled randomly.
    /// </summary>
    private readonly (int, int)[] _points;

    /// <summary>
    /// The index of the current point in the _points array.
    /// </summary>
    private int _currentPointIndex = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="RandomAgent"/> class.
    /// </summary>
    /// <param name="board">The game board.</param>
    public RandomAgent(Board board)
    {
        _board = board;

        // Create an array of all possible points on the board.
        _points = new (int, int)[_board.Width * _board.Height];

        int i = 0;
        for (int x = 0; x < _board.Width; x++)
        {
            for (int y = 0; y < _board.Height; y++)
            {
                _points[i] = (x, y);
                i++;
            }
        }

        // Shuffle the points array to randomize the order of moves.
        _random.Shuffle(_points);
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

    // <summary>
    /// Generates a new game board with the specified width, height, and ships.
    /// The method attempts to place each ship randomly on the board, ensuring that
    /// the ship fits within the board boundaries and does not overlap with any other ships.
    /// If a ship cannot be placed after a certain number of attempts, it is ignored.
    /// </summary>
    /// <param name="width">The width of the game board.</param>
    /// <param name="height">The height of the game board.</param>
    /// <param name="shipsToPlace">An array of ships to be placed on the board.</param>
    /// <returns>The generated game board with the ships placed.</returns>
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

    /// <summary>
    /// Makes a move on the game board by choosing a random point from the _points array.
    /// </summary>
    public void MakeMove()
    {
        var (x, y) = GetRandomPoint();
        _board.Hit((uint)x, (uint)y);
    }

    /// <summary>
    /// Retrieves the next random point from the _points array.
    /// </summary>
    /// <returns>The next random point to hit on the board.</returns>
    private (int, int) GetRandomPoint() => _points[_currentPointIndex++];
}