using SeaBattle.Domain.AI;
using SeaBattle.Domain.StateMachinePattern;

namespace SeaBattle.Application.GameStates.States;

public class EnemyMoveState : IState
{
    private readonly IAIAgent _agent;
    public EnemyMoveState(IAIAgent agent)
    {
        _agent = agent;
    }

    public void Enter()
    {
        _agent.MakeMove();
    }
}
