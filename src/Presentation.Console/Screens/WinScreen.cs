using Presentation.Console.Resources;

namespace SeaBattle.Presentation.Console.Screens;


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
        _subText = TextsRU.WinPromt.Replace("\\n", Environment.NewLine) + Environment.NewLine + TextsRU.EndGamePromt.Replace("\\n", Environment.NewLine);
    }
}
