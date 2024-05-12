using System.Diagnostics;

namespace SeaBattle.PresentationConsole.Screens;

/// <summary>
/// Represents a base class for screen views in a console application.
/// </summary>
internal abstract class ScreenView
{
    /// <summary>
    /// Shows the screen view.
    /// </summary>
    public virtual void Show()
    {
        Debug.WriteLine($"[{GetType().Name}] Show");
    }

    /// <summary>
    /// Hides the screen view by clearing the console and outputting a message to the debug output.
    /// </summary>
    public virtual void Hide()
    {
        Console.Clear();
        Debug.WriteLine($"[{GetType().Name}] Hide");
    }
}
