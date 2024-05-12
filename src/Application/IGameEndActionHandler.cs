using SeaBattle.Domain;

namespace SeaBattle.Application;

/// <summary>
/// Defines a handler for game end actions.
/// </summary>
public interface IGameEndActionHandler : IControllHandler
{
    /// <summary>
    /// Event that is triggered when a restart action is handled.
    /// </summary>
    public event Action OnRestartHandle;

    /// <summary>
    /// Event that is triggered when a quiet action is handled.
    /// </summary>
    public event Action OnQuietHandle;
}
