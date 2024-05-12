using SeaBattle.Application.PlayerMoves;
using SeaBattle.Domain.StateMachinePattern;

namespace SeaBattle.Application.GameStates.States;

public class PlayerMoveState : IState, IDisposable
{
    private readonly IPlayerMovesHandler _movesHandler;
    private readonly PlayerMovesController _movesController;
    public PlayerMoveState(IPlayerMovesHandler movesHandler, PlayerMovesController movesController)
    {
        _movesHandler = movesHandler;
        _movesController = movesController;

        _movesHandler.OnMovePress += _movesController.MoveCursor;
        _movesHandler.OnShootPress += () => _movesController.DoShoot();
    }

    public void Enter()
    {
        _movesHandler.Enabaled = true;
    }

    public void Exit()
    {
        _movesHandler.Enabaled = false;
    }

    public void Dispose()
    {
        _movesHandler.OnMovePress -= _movesController.MoveCursor;
        _movesHandler.OnShootPress -= () => _movesController.DoShoot();
    }
}
