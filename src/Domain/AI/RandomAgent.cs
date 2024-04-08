namespace SeaBattle.Domain.AI;

/// <summary>
/// Represents an AI agent that makes random moves on the game board.
/// This agent maintains a shuffled list of all possible points on the board
/// and chooses the next move randomly from this list.
/// </summary>
public sealed class RandomAgent : IAIAgent
{
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
        var random = new Random();

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
        random.Shuffle(_points);
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