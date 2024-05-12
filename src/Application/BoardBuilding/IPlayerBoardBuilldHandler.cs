using SeaBattle.Domain;
using SeaBattle.Domain.BoardNavigation;

namespace SeaBattle.Application.BoardBuilding;

/// <summary>
/// Represents a handler for player interactions during the board building phase.
/// </summary>
public interface IPlayerBoardBuilldHandler : IControllHandler
{
    /// <summary>
    /// Event that is triggered when the rotate button is pressed.
    /// </summary>
    public event Action OnRotatePress;

    /// <summary>
    /// Event that is triggered when a move button is pressed in a specific direction.
    /// </summary>
    /// <param name="direction">The direction in which the move is to be performed.</param>
    public event Action<MoveDirection> OnMovePress;

    /// <summary>
    /// Event that is triggered when the place button is pressed.
    /// </summary>
    public event Action OnPlacePress;
}
