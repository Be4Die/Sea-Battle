using SeaBattle.Domain;
using SeaBattle.Domain.BoardNavigation;

namespace SeaBattle.Application.PlayerMoves;

/// <summary>
/// Defines a handler for player moves in the game.
/// </summary>
public interface IPlayerMovesHandler : IControllHandler
{
    /// <summary>
    /// Event that is triggered when the player wants to move in a specific direction.
    /// </summary>
    public event Action<MoveDirection> OnMovePress;

    /// <summary>
    /// Event that is triggered when the player wants to shoot.
    /// </summary>
    public event Action OnShootPress;
}
