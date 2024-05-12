using Presentation.Console.Resources;

namespace SeaBattle.PresentationConsole.Screens;


/// <summary>
/// Represents a screen that displays a win message in a console application.
/// </summary>
internal class WinScreen : LogoScreen
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WinScreen"/> class.
    /// </summary>
    public WinScreen()
    {
        _logoText = TextsRU.WinPlayerLabel.Replace("\\r\\n", Environment.NewLine);
        _subText = TextsRU.WinPromt + Environment.NewLine + TextsRU.EndGamePromt;
    }
}
