using SeaBattle.Application.Contexts;
using SeaBattle.Domain.GameBoard;

namespace SeaBattle.Application.PlayerMoves;

internal static class PlayerMovesControllerFactory
{
    public static PlayerMovesController Create() => new (
        GameContext.ResolveById<Board>(GameContext.EnemyBoardId));
}
