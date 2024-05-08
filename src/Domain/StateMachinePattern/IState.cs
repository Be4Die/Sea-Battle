namespace SeaBattle.Domain.StateMachinePattern;

public interface IState
{
    public void Enter() { }
    public void Exit() { }
    public void Update() { }
}
