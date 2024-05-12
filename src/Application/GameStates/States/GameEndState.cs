using SeaBattle.Domain.StateMachinePattern;

namespace SeaBattle.Application.GameStates.States;

public abstract class GameEndState : IState
{
    protected readonly IGameEndActionHandler _restartHandler;

    protected GameEndState(IGameEndActionHandler restartHandler)
    {
        _restartHandler = restartHandler;
    }

    public void Enter()
    {
        _restartHandler.Enabaled = true;
    }

    public void Exit()
    {
        _restartHandler.Enabaled = false;
    }
}
