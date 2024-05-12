using SeaBattle.Application.BoardBuilding;
using SeaBattle.Domain;
using SeaBattle.Domain.AI;
using SeaBattle.Domain.GameBoard;
using SeaBattle.Domain.StateMachinePattern;

namespace SeaBattle.Application.GameStates.States;

public class RestartState : IState 
{
    private readonly Board _playerBoard;
    private readonly Board _enemyBoard;
    private readonly IAIAgent _agent;
    private readonly Ship[] _shipsWhenRestart;
    private readonly PlayerBoardBuilder _playerBoardBuilder;

    public RestartState(Board playerBoard, Board enemyBoard, IAIAgent agent, Ship[] shipsWhenRestart, PlayerBoardBuilder boardBuilder)
    {
        _playerBoard = playerBoard;
        _enemyBoard = enemyBoard;
        _agent = agent;
        _shipsWhenRestart = shipsWhenRestart;
        _playerBoardBuilder = boardBuilder;
    }

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
