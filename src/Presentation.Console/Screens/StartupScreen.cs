using Presentation.Console.Resources;

namespace SeaBattle.PresentationConsole.Screens;

internal sealed class StartupScreen : LogoScreen
{
    public StartupScreen()
    {
        _logoText = TextsRU.Logo.Replace("\\r\\n", Environment.NewLine);
        _subText = TextsRU.StartPromt;
    }
}
