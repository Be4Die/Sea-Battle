namespace SeaBattle.Domain.StateMachinePattern;


/// <summary>
/// Represents a state machine that manages different states.
/// </summary>
public interface IStateMachine
{
    // <summary>
    /// Event that is triggered when the state changes.
    /// </summary>
    public event Action<Type> OnStateChanged;

    /// <summary>
    /// Gets the type of the current state.
    /// </summary>
    public Type? CurrentStateType { get; }

    /// <summary>
    /// Changes the current state to the specified state type.
    /// </summary>
    /// <typeparam name="TState">The type of the state to change to.</typeparam>
    public void Change<TState>() where TState : IState;
}
