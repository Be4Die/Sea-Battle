using SeaBattle.Application.BoardBuilding;
using SeaBattle.Domain.StateMachinePattern;

namespace SeaBattle.Application.GameStates.States;

/// <summary>
/// Represents a state in the game state machine where the player is building their board.
/// </summary>
public class BuildingBoardState : IState
{
    private readonly IPlayerBoardBuilldHandler _builldHandler;
    private readonly PlayerBoardBuilder _boardBuilder;


    /// <summary>
    /// Initializes a new instance of the <see cref="BuildingBoardState"/> class with the specified board builder and player board build handler.
    /// </summary>
    /// <param name="boardBuilder">The board builder that will be used to build the player's board.</param>
    /// <param name="builldHandler">The player board build handler that will handle player input for building the board.</param>
    public BuildingBoardState(PlayerBoardBuilder boardBuilder, IPlayerBoardBuilldHandler builldHandler)
    {

        _boardBuilder = boardBuilder;
        _builldHandler = builldHandler;

        _builldHandler.OnPlacePress += _boardBuilder.PlaceShip;
        _builldHandler.OnRotatePress += _boardBuilder.RotateShip;
        _builldHandler.OnMovePress += _boardBuilder.MoveShip;
    }

    /// <summary>
    /// Enters the building board state by enabling the player board build handler.
    /// </summary>
    public void Enter() => _builldHandler.Enabaled = true;

    /// <summary>
    /// Exits the building board state by disabling the player board build handler.
    /// </summary>
    public void Exit() => _builldHandler.Enabaled = false;
}
