using SeaBattle.Domain.StateMachinePattern;

namespace SeaBattle.Application.GameStates.States;

public class SetupState : IState 
{
    private readonly IGameSetupHandler _setupHandler;
    public SetupState(IGameSetupHandler setupHandler)
    {
        _setupHandler = setupHandler;
    }

    public void Enter()
    {
        _setupHandler.Enabaled = true;
    }

    public void Exit()
    {
        _setupHandler.Enabaled = false;
    }
}
