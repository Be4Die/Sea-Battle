using SeaBattle.Domain;
using SeaBattle.Domain.BoardNavigation;

namespace SeaBattle.Application.PlayerMoves;

public interface IPlayerMovesHandler : IControllHandler
{
    public event Action<MoveDirection> OnMovePress;
    public event Action OnShootPress;
}
