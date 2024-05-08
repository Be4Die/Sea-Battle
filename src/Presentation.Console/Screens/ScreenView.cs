using System.Diagnostics;

namespace SeaBattle.PresentationConsole.Screens;

internal abstract class ScreenView
{
    public virtual void Show()
    {
        Console.Clear();
        Debug.WriteLine($"[{GetType().Name}] Show");
    }

    public virtual void Hide()
    {
        Debug.WriteLine($"[{GetType().Name}] Hide");
    }
}
