using SeaBattle.Domain.AI;
using SeaBattle.Domain.StateMachinePattern;

namespace SeaBattle.Application.GameStates.States;

public class EnemyMove : IState
{
    private readonly IAIAgent _agent;
    public EnemyMove(IAIAgent agent)
    {
        _agent = agent;
    }

    public void Enter()
    {
        _agent.MakeMove();
    }
}
