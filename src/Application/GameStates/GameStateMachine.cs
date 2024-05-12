using SeaBattle.Application.BoardBuilding;
using SeaBattle.Application.GameStates.States;
using SeaBattle.Application.PlayerMoves;
using SeaBattle.Domain;
using SeaBattle.Domain.AI;
using SeaBattle.Domain.GameBoard;
using SeaBattle.Domain.StateMachinePattern;
using System.Diagnostics;

namespace SeaBattle.Application.GameStates;

/// <summary>
/// Represents the game state machine that manages the different states of the game.
/// </summary>
public class GameStateMachine : IStateMachine
{
    /// <summary>
    /// Event that is triggered when the state of the game state machine changes.
    /// </summary>
    public event Action<Type>? OnStateChanged;

    /// <summary>
    /// Gets the type of the current state.
    /// </summary>
    public Type? CurrentStateType { get; private set; }

    private IState? _currentState;
    private Dictionary<Type, IState> _states = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="GameStateMachine"/> class with the necessary components for managing the game states.
    /// </summary>
    /// <param name="movesHandler">The player moves handler.</param>
    /// <param name="movesController">The moves controller.</param>
    /// <param name="boardBuilder">The player board builder.</param>
    /// <param name="builldHandler">The player board build handler.</param>
    /// <param name="aIAgent">The AI agent.</param>
    /// <param name="setupHandler">The game setup handler.</param>
    /// <param name="playerBoard">The player's game board.</param>
    /// <param name="enemyBoard">The enemy's game board.</param>
    /// <param name="shipsForRestart">The array of ships to be used when restarting the game.</param>
    /// <param name="restartHandler">The game end action handler for restarting the game.</param>
    public GameStateMachine(IPlayerMovesHandler movesHandler, PlayerMovesController movesController, 
        PlayerBoardBuilder boardBuilder, IPlayerBoardBuilldHandler builldHandler,
        IAIAgent aIAgent, IGameSetupHandler setupHandler,
        Board playerBoard, Board enemyBoard,
        Ship[] shipsForRestart, IGameEndActionHandler restartHandler)
    {
        _states[typeof(BuildingBoardState)] = new BuildingBoardState(boardBuilder, builldHandler);
        _states[typeof(PlayerMoveState)] = new PlayerMoveState(movesHandler, movesController);
        _states[typeof(EnemyMoveState)] = new EnemyMoveState(aIAgent);
        _states[typeof(ExitState)] = new ExitState();
        _states[typeof(SetupState)] = new SetupState(setupHandler);
        _states[typeof(RestartState)] = new RestartState(playerBoard, enemyBoard, aIAgent, shipsForRestart, boardBuilder);
        _states[typeof(PlayerWinState)] = new PlayerWinState(restartHandler);
        _states[typeof(EnemyWinState)] = new EnemyWinState(restartHandler);
    }

    /// <summary>
    /// Changes the current state of the game state machine to the specified state type.
    /// </summary>
    /// <typeparam name="TState">The type of the state to change to.</typeparam>
    /// <exception cref="UnknownStateException">Thrown when the specified state type is not recognized.</exception>
    public void Change<TState>() where TState : IState
    {
        if (!_states.ContainsKey(typeof(TState)))
        {
            Debug.WriteLine($"UnknownStateException {typeof(TState).Name}");
            throw new UnknownStateException(typeof(TState));
        }

        Debug.WriteLine($"[{nameof(GameStateMachine)}] Change state to {typeof(TState).Name}");
        _currentState?.Exit();
        _currentState = _states[typeof(TState)];
        _currentState.Enter();
        CurrentStateType = typeof(TState);
        OnStateChanged?.Invoke(typeof(TState));
    }
}
