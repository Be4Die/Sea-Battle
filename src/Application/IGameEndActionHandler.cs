using SeaBattle.Domain;

namespace SeaBattle.Application;

public interface IGameEndActionHandler : IControllHandler
{
    public event Action OnRestartHandle;
    public event Action OnQuietHandle;
}
