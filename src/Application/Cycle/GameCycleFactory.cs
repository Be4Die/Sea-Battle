using SeaBattle.Application.BoardBuilding;
using SeaBattle.Application.Contexts;
using SeaBattle.Application.GameStates;
using SeaBattle.Application.PlayerMoves;
using SeaBattle.Domain.GameBoard;

namespace SeaBattle.Application.Cycle;

/// <summary>
/// Factory for creating instances of the <see cref="GameCycle"/> class.
/// </summary>
internal class GameCycleFactory
{
    /// <summary>
    /// Creates a new instance of the <see cref="GameCycle"/> class using the services resolved from the game context.
    /// </summary>
    /// <returns>A new instance of the <see cref="GameCycle"/> class.</returns>
    internal GameCycle Create() => new GameCycle(
        GameContext.ResolveSingle<GameStateMachine>(),
        GameContext.ResolveSingle<IGameSetupHandler>(),
        GameContext.ResolveSingle<PlayerBoardBuilder>(),
        GameContext.ResolveSingle<PlayerMovesController>(),
        GameContext.ResolveById<Board>(GameContext.PlayerBoardId),
        GameContext.ResolveById<Board>(GameContext.EnemyBoardId),
        GameContext.ResolveSingle<IGameEndActionHandler>());
}
