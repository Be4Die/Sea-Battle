namespace SeaBattle.Domain.StateMachinePattern;

public interface IStateMachine
{
    public event Action<Type> OnStateChanged;
    public Type? CurrentStateType { get; }
    public void Change<TState>() where TState : IState;
}
