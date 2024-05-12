using SeaBattle.Domain.AI;
using SeaBattle.Domain.StateMachinePattern;

namespace SeaBattle.Application.GameStates.States;

/// <summary>
/// Represents a state in the game state machine where the enemy makes a move.
/// </summary>
public class EnemyMoveState : IState
{
    private readonly IAIAgent _agent;

    /// <summary>
    /// Initializes a new instance of the <see cref="EnemyMoveState"/> class with the specified AI agent.
    /// </summary>
    /// <param name="agent">The AI agent that will make the move.</param>
    public EnemyMoveState(IAIAgent agent)
    {
        _agent = agent;
    }

    /// <summary>
    /// Enters the enemy move state by having the AI agent make a move.
    /// </summary>
    public void Enter() => _agent.MakeMove();
}
