using SeaBattle.Application.Contexts;
using SeaBattle.Domain;
using SeaBattle.Domain.GameBoard;

namespace SeaBattle.Application.BoardBuilding;

internal static class PlayerBoardBuilderFactory
{
    public static PlayerBoardBuilder Create() => new (
        GameContext.ResolveById<Board>(GameContext.PlayerBoardId),
        GameContext.ResolveSingle<Ship[]>());
}
