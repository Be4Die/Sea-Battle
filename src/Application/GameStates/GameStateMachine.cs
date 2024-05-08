using SeaBattle.Application.BoardBuilding;
using SeaBattle.Application.GameStates.States;
using SeaBattle.Application.PlayerMoves;
using SeaBattle.Domain.AI;
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
        IAIAgent aIAgent, IGameSetupHandler setupHandler)
    {
        _states[typeof(BuildingBoardState)] = new BuildingBoardState(boardBuilder, builldHandler);
        _states[typeof(PlayerMoveState)] = new PlayerMoveState(movesHandler, movesController);
        _states[typeof(EnemyMove)] = new EnemyMove(aIAgent);
        _states[typeof(ExitState)] = new ExitState();
        _states[typeof(SetupState)] = new SetupState(setupHandler);
    }

    public void Change<TState>() where TState : IState
    {
        if (!_states.ContainsKey(typeof(TState)))
            throw new UnknownStateException(typeof(TState));

        Debug.WriteLine($"[{nameof(GameStateMachine)}] Change state to {typeof(TState).Name}");
        _currentState?.Exit();
        _currentState = _states[typeof(TState)];
        _currentState.Enter();
        OnStateChanged?.Invoke(typeof(TState));
    }
}
