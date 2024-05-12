using SeaBattle.Application.BoardBuilding;
using SeaBattle.Application.Contexts;
using SeaBattle.Application.PlayerMoves;
using SeaBattle.Domain;
using SeaBattle.Domain.AI;
using SeaBattle.Domain.GameBoard;

namespace SeaBattle.Application.GameStates;

/// <summary>
/// Factory class for creating instances of the <see cref="GameStateMachine"/> class.
/// </summary>
internal static class GameStateMachineFactory
{
    /// <summary>
    /// Creates a new instance of the <see cref="GameStateMachine"/> class with dependencies resolved from the <see cref="GameContext"/>.
    /// </summary>
    /// <returns>A new instance of the <see cref="GameStateMachine"/> class.</returns>
    public static GameStateMachine Create() => new (
        GameContext.ResolveSingle<IPlayerMovesHandler>(),
        GameContext.ResolveSingle<PlayerMovesController>(),
        GameContext.ResolveSingle<PlayerBoardBuilder>(),
        GameContext.ResolveSingle<IPlayerBoardBuilldHandler>(),
        GameContext.ResolveSingle<IAIAgent>(),
        GameContext.ResolveSingle<IGameSetupHandler>(),
        GameContext.ResolveById<Board>(GameContext.PlayerBoardId),
        GameContext.ResolveById<Board>(GameContext.EnemyBoardId),
        GameContext.ResolveSingle<Ship[]>(),
        GameContext.ResolveSingle<IGameEndActionHandler>());
}
