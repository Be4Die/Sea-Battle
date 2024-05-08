using SeaBattle.Application.BoardBuilding;
using SeaBattle.Application.Contexts;
using SeaBattle.Application.PlayerMoves;
using SeaBattle.Domain.AI;

namespace SeaBattle.Application.GameStates;

internal class GameStateMachineFactory
{
    public GameStateMachine Create() => new GameStateMachine(
        GameContext.ResolveSingle<IPlayerMovesHandler>(),
        GameContext.ResolveSingle<PlayerMovesController>(),
        GameContext.ResolveSingle<PlayerBoardBuilder>(),
        GameContext.ResolveSingle<IPlayerBoardBuilldHandler>(),
        GameContext.ResolveSingle<IAIAgent>(),
        GameContext.ResolveSingle<IGameSetupHandler>());
}
