using SeaBattle.Domain;
using SeaBattle.Domain.BoardNavigation;

namespace SeaBattle.Application.BoardBuilding;

public interface IPlayerBoardBuilldHandler : IControllHandler
{
    public event Action OnRotatePress;
    public event Action<MoveDirection> OnMovePress;
    public event Action OnPlacePress;
}
