using SeaBattle.Application.BoardBuilding;
using SeaBattle.Domain;
using SeaBattle.Domain.AI;
using SeaBattle.Domain.GameBoard;
using SeaBattle.Domain.StateMachinePattern;

namespace SeaBattle.Application.GameStates.States;

/// <summary>
/// Represents a state in the game state machine where the game is being restarted.
/// </summary>
public class RestartState : IState 
{
    private readonly Board _playerBoard;
    private readonly Board _enemyBoard;
    private readonly IAIAgent _agent;
    private readonly Ship[] _shipsWhenRestart;
    private readonly PlayerBoardBuilder _playerBoardBuilder;

    /// <summary>
    /// Initializes a new instance of the <see cref="RestartState"/> class with the necessary components for restarting the game.
    /// </summary>
    /// <param name="playerBoard">The player's game board.</param>
    /// <param name="enemyBoard">The enemy's game board.</param>
    /// <param name="agent">The AI agent for the enemy.</param>
    /// <param name="shipsWhenRestart">The array of ships to be used when restarting the game.</param>
    /// <param name="boardBuilder">The player board builder.</param>
    public RestartState(Board playerBoard, Board enemyBoard, IAIAgent agent, 
        Ship[] shipsWhenRestart, PlayerBoardBuilder boardBuilder)
    {
        _playerBoard = playerBoard;
        _enemyBoard = enemyBoard;
        _agent = agent;
        _shipsWhenRestart = shipsWhenRestart;
        _playerBoardBuilder = boardBuilder;
    }

    /// <summary>
    /// Enters the restart state by clearing the player and enemy boards, resetting the ships, and restarting the game.
    /// </summary>
    public void Enter()
    {
        _playerBoard.Clear();
        _enemyBoard.Clear();
        foreach (var ship in _shipsWhenRestart)
        {
            ship.SetShipAlive();
        }
        _agent.FillBoard(_enemyBoard, _shipsWhenRestart);
        _playerBoardBuilder.Restart();
    }
}
