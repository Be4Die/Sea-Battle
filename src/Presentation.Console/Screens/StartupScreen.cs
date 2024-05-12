using Presentation.Console.Resources;

namespace SeaBattle.Presentation.Console.Screens;

/// <summary>
/// Represents a screen that displays a startup message in a console application.
/// </summary>
internal sealed class StartupScreen : LogoScreen
{
    /// <summary>
    /// Initializes a new instance of the <see cref="StartupScreen"/> class.
    /// </summary>
    public StartupScreen()
    {
        _logoText = TextsRU.Logo.Replace("\\r\\n", Environment.NewLine);
        _subText = TextsRU.StartPromt;
    }
}
