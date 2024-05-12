using SeaBattle.Domain.GameBoard;
using SeaBattle.Domain.BoardNavigation;

namespace SeaBattle.Application.PlayerMoves;

/// <summary>
/// Controls the player's moves on the game board.
/// </summary>
public class PlayerMovesController
{
    /// <summary>
    /// Event that is triggered when a move has been completed.
    /// </summary>
    public event Action? OnMoveCompleted;

    /// <summary>
    /// Event that is triggered when the player moves in a specific direction.
    /// </summary>
    public event Action<MoveDirection>? OnMoved;

    /// <summary>
    /// Gets the cursor representing the player's current position on the board.
    /// </summary>
    public Cursor Cursor { get; private set; } = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerMovesController"/> class with the specified game board.
    /// </summary>
    /// <param name="board">The game board on which the player moves.</param>
    private readonly Board _board;

    /// <summary>
    /// Moves the cursor in the specified direction and updates the player's position on the board.
    /// </summary>
    /// <param name="direction">The direction in which to move the cursor.</param>
    public PlayerMovesController(Board board)
    {
        _board = board;
        Cursor.ChoosenElement = _board.GetCeil(0, 0);
    }

    /// <summary>
    /// Moves the cursor in the specified direction and updates the player's position on the board.
    /// </summary>
    /// <param name="direction">The direction in which to move the cursor.</param>
    public void MoveCursor(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.Left: Cursor.X = (int)Math.Clamp(Cursor.X - 1, 0, _board.Width-1); break;
            case MoveDirection.Right: Cursor.X = (int)Math.Clamp(Cursor.X + 1, 0, _board.Width-1); break;
            case MoveDirection.Up: Cursor.Y = (int)Math.Clamp(Cursor.Y - 1, 0, _board.Height-1); break;
            case MoveDirection.Down: Cursor.Y = (int)Math.Clamp(Cursor.Y + 1, 0, _board.Height - 1); break;
            default: throw new NotImplementedException();
        }
        Cursor.ChoosenElement = _board.GetCeil((uint)Cursor.X, (uint)Cursor.Y);

        OnMoved?.Invoke(direction);
    }

    /// <summary>
    /// Attempts to perform a shoot action at the current cursor position.
    /// </summary>
    /// <returns>True if the shoot action was successful; otherwise, false.</returns>
    public bool DoShoot()
    {
        if (Cursor.ChoosenElement == null || Cursor.ChoosenElement.IsHited)
            return false;   

        OnMoveCompleted?.Invoke();
        return Cursor.ChoosenElement.Hit();
    }
}
