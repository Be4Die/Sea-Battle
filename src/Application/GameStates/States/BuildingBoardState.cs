using SeaBattle.Application.BoardBuilding;
using SeaBattle.Domain.StateMachinePattern;

namespace SeaBattle.Application.GameStates.States;

public class BuildingBoardState : IState
{
    private readonly IPlayerBoardBuilldHandler _builldHandler;
    private readonly PlayerBoardBuilder _boardBuilder;

    public BuildingBoardState(PlayerBoardBuilder boardBuilder, IPlayerBoardBuilldHandler builldHandler)
    {

        _boardBuilder = boardBuilder;
        _builldHandler = builldHandler;

        _builldHandler.OnPlacePress += _boardBuilder.PlaceShip;
        _builldHandler.OnRotatePress += _boardBuilder.RotateShip;
        _builldHandler.OnMovePress += _boardBuilder.MoveShip;
    }

    public void Enter()
    {
        _builldHandler.Enabaled = true;
    }

    public void Exit()
    {
        _builldHandler.Enabaled = false;
    }
}
