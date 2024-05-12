using SeaBattle.Domain.StateMachinePattern;

namespace SeaBattle.Application.GameStates.States;

/// <summary>
/// Represents a state in the game state machine where the game setup is being handled.
/// </summary>
public class SetupState(IGameSetupHandler setupHandler) : IState 
{
    private readonly IGameSetupHandler _setupHandler = setupHandler;

    /// <summary>
    /// Enters the setup state by enabling the setup handler.
    /// </summary>
    public void Enter() => _setupHandler.Enabaled = true;

    /// <summary>
    /// Exits the setup state by disabling the setup handler.
    /// </summary>
    public void Exit() => _setupHandler.Enabaled = false;
}
