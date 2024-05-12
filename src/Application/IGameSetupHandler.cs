using SeaBattle.Domain;

namespace SeaBattle.Application;

/// <summary>
/// Defines a handler for game setup actions.
/// </summary>
public interface IGameSetupHandler : IControllHandler
{
    /// <summary>
    /// Event that is triggered when the game setup has ended.
    /// </summary>
    public event Action OnSetupEnd;
}
