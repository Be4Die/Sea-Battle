using SeaBattle.Domain.StateMachinePattern;

namespace SeaBattle.Application.GameStates.States;

/// <summary>
/// Represents a state in the game state machine where the game setup is being handled.
/// </summary>
public class SetupState : IState 
{
    private readonly IGameSetupHandler _setupHandler;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetupState"/> class with the specified setup handler.
    /// </summary>
    /// <param name="setupHandler">The setup handler that will be used to handle the game setup.</param>
    public SetupState(IGameSetupHandler setupHandler)
    {
        _setupHandler = setupHandler;
    }

    /// <summary>
    /// Enters the setup state by enabling the setup handler.
    /// </summary>
    public void Enter() => _setupHandler.Enabaled = true;

    /// <summary>
    /// Exits the setup state by disabling the setup handler.
    /// </summary>
    public void Exit() => _setupHandler.Enabaled = false;
}
