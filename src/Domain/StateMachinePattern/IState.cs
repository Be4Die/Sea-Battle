namespace SeaBattle.Domain.StateMachinePattern;

/// <summary>
/// Represents a state in a state machine.
/// </summary>
public interface IState
{
    /// <summary>
    /// Called when the state is entered.
    /// </summary>
    public void Enter() { }

    /// <summary>
    /// Called when the state is exited.
    /// </summary>
    public void Exit() { }

    /// <summary>
    /// Called every frame while the state is active.
    /// </summary>
    public void Update() { }
}
