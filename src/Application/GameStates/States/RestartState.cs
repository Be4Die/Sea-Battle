using SeaBattle.Application.BoardBuilding;
using SeaBattle.Domain;
using SeaBattle.Domain.AI;
using SeaBattle.Domain.GameBoard;
using SeaBattle.Domain.StateMachinePattern;

namespace SeaBattle.Application.GameStates.States;

/// <summary>
/// Represents a state in the game state machine where the game is being restarted.
/// </summary>
public class RestartState(Board playerBoard, Board enemyBoard, IAIAgent agent,
        Ship[] shipsWhenRestart, PlayerBoardBuilder boardBuilder) : IState 
{
    private readonly Board _playerBoard = playerBoard;
    private readonly Board _enemyBoard = enemyBoard;
    private readonly IAIAgent _agent = agent;
    private readonly Ship[] _shipsWhenRestart = shipsWhenRestart;
    private readonly PlayerBoardBuilder _playerBoardBuilder = boardBuilder;

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
