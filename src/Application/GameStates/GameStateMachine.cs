using SeaBattle.Application.BoardBuilding;
using SeaBattle.Application.GameStates.States;
using SeaBattle.Application.PlayerMoves;
using SeaBattle.Domain;
using SeaBattle.Domain.AI;
using SeaBattle.Domain.GameBoard;
using SeaBattle.Domain.StateMachinePattern;
using System.Diagnostics;

namespace SeaBattle.Application.GameStates;

public class GameStateMachine : IStateMachine
{
    public event Action<Type>? OnStateChanged;

    public Type? CurrentStateType { get; private set; }

    private IState? _currentState;
    private Dictionary<Type, IState> _states = new ();


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
