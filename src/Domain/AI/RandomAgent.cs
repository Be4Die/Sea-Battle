using SeaBattle.Domain;

namespace Domain.AI;

internal sealed class RandomAgent : IAIAgent
{
    private readonly Board _board;

    private readonly (int, int)[] _points;

    private int _currentPointIndex = 0;

    public RandomAgent(Board board)
    {
        _board = board;
        var random = new Random();


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

        random.Shuffle(_points);

    }


    public void MakeMove()
    {
        var (x,y) = GetRandomPoint();

        _board.Hit((uint)x, (uint)y);
    }

    private (int, int) GetRandomPoint() => _points[_currentPointIndex++];
}
