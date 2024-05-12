using SeaBattle.Application.PlayerMoves;
using SeaBattle.Domain.StateMachinePattern;

namespace SeaBattle.Application.GameStates.States;

/// <summary>
/// Represents a state in the game state machine where the player is allowed to make moves.
/// </summary>
public class PlayerMoveState : IState, IDisposable
{
    private readonly IPlayerMovesHandler _movesHandler;
    private readonly PlayerMovesController _movesController;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerMoveState"/> class with the specified player moves handler and moves controller.
    /// </summary>
    /// <param name="movesHandler">The player moves handler that will handle player input.</param>
    /// <param name="movesController">The moves controller that will process the player's moves.</param>
    public PlayerMoveState(IPlayerMovesHandler movesHandler, PlayerMovesController movesController)
    {
        _movesHandler = movesHandler;
        _movesController = movesController;

        _movesHandler.OnMovePress += _movesController.MoveCursor;
        _movesHandler.OnShootPress += () => _movesController.DoShoot();
    }

    /// <summary>
    /// Enters the player move state by enabling the player moves handler.
    /// </summary>
    public void Enter() => _movesHandler.Enabaled = true;

    /// <summary>
    /// Exits the player move state by disabling the player moves handler.
    /// </summary>
    public void Exit() => _movesHandler.Enabaled = false;

    /// <summary>
    /// Disposes of the player move state by unsubscribing from the player moves handler events.
    /// </summary>
    public void Dispose()
    {
        _movesHandler.OnMovePress -= _movesController.MoveCursor;
        _movesHandler.OnShootPress -= () => _movesController.DoShoot();
    }
}
