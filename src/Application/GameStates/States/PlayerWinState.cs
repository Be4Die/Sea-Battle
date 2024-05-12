namespace SeaBattle.Application.GameStates.States;

/// <summary>
/// Represents a state in the game state machine where the player has won the game.
/// </summary>
public class PlayerWinState(IGameEndActionHandler restartHandler) : GameEndState(restartHandler) { }
