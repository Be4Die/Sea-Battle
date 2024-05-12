using Presentation.Console.Resources;

namespace SeaBattle.PresentationConsole.Screens;

internal class LoseScreen : LogoScreen
{
    public LoseScreen()
    {
        _logoText = TextsRU.WinEnemyLabel.Replace("\\r\\n", Environment.NewLine);
        _subText = TextsRU.LosePromt + Environment.NewLine + TextsRU.EndGamePromt;
    }
}
