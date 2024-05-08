using SeaBattle.Application.Contexts;
using SeaBattle.Domain.GameBoard;

namespace SeaBattle.Application.PlayerMoves;

internal class PlayerMovesControllerFactory
{
    public PlayerMovesController Create() => new PlayerMovesController(
        GameContext.ResolveById<Board>(GameContext.PlayerBoardId));
}
