using Presentation.Console.Resources;

namespace SeaBattle.PresentationConsole.Screens;

internal class WinScreen : LogoScreen
{
    public WinScreen()
    {
        _logoText = TextsRU.WinPlayerLabel.Replace("\\r\\n", Environment.NewLine);
        _subText = TextsRU.WinPromt + Environment.NewLine + TextsRU.EndGamePromt;
    }
}
