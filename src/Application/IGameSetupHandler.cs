using SeaBattle.Domain;

namespace SeaBattle.Application;

public interface IGameSetupHandler : IControllHandler
{
    public event Action OnSetupEnd;
}
