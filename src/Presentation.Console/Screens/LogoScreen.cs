namespace SeaBattle.PresentationConsole.Screens;

/// <summary>
/// Represents a screen that displays a logo and additional text in a console application.
/// </summary>
internal class LogoScreen : ScreenView
{
    /// <summary>
    /// The text to be displayed as the logo.
    /// </summary>
    protected string _logoText = "";

    /// <summary>
    /// Additional text to be displayed below the logo.
    /// </summary>
    protected string _subText = "";

    /// <summary>
    /// The number of empty lines to be inserted between the logo and the subtext.
    /// </summary>
    protected int _spaces = 2;

    /// <summary>
    /// Initializes a new instance of the <see cref="LogoScreen"/> class with the specified logo text, subtext, and number of spaces.
    /// </summary>
    /// <param name="logo">The text to be displayed as the logo.</param>
    /// <param name="sub">The additional text to be displayed below the logo.</param>
    /// <param name="spaces">The number of empty lines to be inserted between the logo and the subtext.</param>
    public LogoScreen(string logo, string sub = "", int spaces = 2)
    {
        _logoText = logo;
        _subText = sub;
        _spaces = spaces;
    }

    /// <summary>
    /// Shows the logo screen by outputting the logo text, a specified number of empty lines, and the subtext to the console.
    /// </summary>
    public override void Show()
    {
        base.Show();
        ConsoleTools.SetConsoleSizeFromText(_logoText, setHeight: false);

        Console.WriteLine(_logoText);
        for (int i = 0; i < _spaces; i++)
        {
            Console.WriteLine();
        }
        Console.WriteLine(_subText);
    }
}
