using SeaBattle.Application.Contexts;
using SeaBattle.Domain;
using SeaBattle.Domain.GameBoard;

namespace SeaBattle.Application.BoardBuilding;

internal class PlayerBoardBuilderFactory
{
    public PlayerBoardBuilder Create() => new PlayerBoardBuilder(
        GameContext.ResolveById<Board>(GameContext.PlayerBoardId),
        GameContext.ResolveSingle<Ship[]>());
}
