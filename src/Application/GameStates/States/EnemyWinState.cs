namespace SeaBattle.Application.GameStates.States;

/// <summary>
/// Represents a state in the game state machine where the enemy has won the game.
/// </summary>
public class EnemyWinState(IGameEndActionHandler restartHandler) : GameEndState(restartHandler) { }