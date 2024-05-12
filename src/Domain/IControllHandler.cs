namespace SeaBattle.Domain;

/// <summary>
/// Represents an entity that handles control events, such as button presses.
/// </summary>
public interface IControllHandler
{
    /// <summary>
    /// Gets or sets a value indicating whether the control handler is enabled.
    /// When disabled, the control handler will not respond to control events.
    /// </summary>
    public bool Enabaled { get; set; }
}
