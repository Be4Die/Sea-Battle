using Presentation.Console.Resources;

namespace SeaBattle.Presentation.Console.Screens;

/// <summary>
/// Represents a screen that displays a lose message in a console application.
/// </summary>
internal class LoseScreen : LogoScreen
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoseScreen"/> class.
    /// </summary>
    public LoseScreen()
    {
        _logoText = TextsRU.WinEnemyLabel.Replace("\\r\\n", Environment.NewLine);
        _subText = TextsRU.LosePromt + Environment.NewLine + TextsRU.EndGamePromt;
    }
}
