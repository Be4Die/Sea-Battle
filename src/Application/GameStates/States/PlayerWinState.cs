namespace SeaBattle.Application.GameStates.States;

/// <summary>
/// Represents a state in the game state machine where the player has won the game.
/// </summary>
public class PlayerWinState : GameEndState
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerWinState"/> class with the specified game end action handler.
    /// </summary>
    /// <param name="restartHandler">The game end action handler that will be used to handle the end of the game.</param>
    public PlayerWinState(IGameEndActionHandler restartHandler) : base(restartHandler) { }
}
