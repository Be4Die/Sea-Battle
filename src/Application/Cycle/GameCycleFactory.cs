using SeaBattle.Application.BoardBuilding;
using SeaBattle.Application.Contexts;
using SeaBattle.Application.GameStates;
using SeaBattle.Application.PlayerMoves;
using SeaBattle.Domain.GameBoard;

namespace SeaBattle.Application.Cycle;

internal class GameCycleFactory
{
    internal GameCycle Create() => new GameCycle(
        GameContext.ResolveSingle<GameStateMachine>(),
        GameContext.ResolveSingle<IGameSetupHandler>(),
        GameContext.ResolveSingle<PlayerBoardBuilder>(),
        GameContext.ResolveSingle<PlayerMovesController>(),
        GameContext.ResolveById<Board>(GameContext.PlayerBoardId),
        GameContext.ResolveById<Board>(GameContext.EnemyBoardId));
}
