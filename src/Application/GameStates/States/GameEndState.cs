﻿using SeaBattle.Domain.StateMachinePattern;

namespace SeaBattle.Application.GameStates.States;

/// <summary>
/// Represents a state in the game state machine that is used to handle the end of the game.
/// </summary>
public abstract class GameEndState(IGameEndActionHandler restartHandler) : IState
{
    /// <summary>
    /// The game end action handler that will be used to handle the end of the game.
    /// </summary>
    protected readonly IGameEndActionHandler _restartHandler = restartHandler;

    /// <summary>
    /// Enters the game end state by enabling the game end action handler.
    /// </summary>
    public void Enter() => _restartHandler.Enabaled = true;

    /// <summary>
    /// Exits the game end state by disabling the game end action handler.
    /// </summary>
    public void Exit() => _restartHandler.Enabaled = false;
}
