using SeaBattle.Domain.GameBoard;
using SeaBattle.Domain.BoardNavigation;

namespace SeaBattle.Application.PlayerMoves;

public class PlayerMovesController
{
    public event Action? OnMoveCompleted;
    public event Action<MoveDirection>? OnMoved;
    public Cursor Cursor { get; private set; } = new ();
    private readonly Board _board;

    public PlayerMovesController(Board board)
    {
        _board = board;
        Cursor.ChoosenElement = _board.GetCeil(0, 0);
    }

    public void MoveCursor(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Left: Cursor.X = (int)Math.Clamp(Cursor.X - 1, 0, _board.Width); break;
            case MoveDirection.Right: Cursor.X = (int)Math.Clamp(Cursor.X + 1, 0, _board.Width); break;
            case MoveDirection.Up: Cursor.Y = (int)Math.Clamp(Cursor.Y - 1, 0, _board.Height); break;
            case MoveDirection.Down: Cursor.Y = (int)Math.Clamp(Cursor.Y + 1, 0, _board.Height); break;
            default: throw new NotImplementedException();
        }
        Cursor.ChoosenElement = _board.GetCeil((uint)Cursor.X, (uint)Cursor.Y);

        OnMoved?.Invoke(direction);
    }

    public bool DoShoot()
    {
        if (Cursor.ChoosenElement == null)
            return false;

        OnMoveCompleted?.Invoke();
        return Cursor.ChoosenElement.Hit();
    }
}
