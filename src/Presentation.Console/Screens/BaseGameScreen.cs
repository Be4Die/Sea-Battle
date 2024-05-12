namespace SeaBattle.Presentation.Console.Screens;

/// <summary>
/// Represents a base class for game screens in a console application.
/// </summary>
internal class BaseGameScreen : ScreenView
{
    /// <summary>
    /// The header text of the screen.
    /// </summary>
    protected string _header = " ";
    /// <summary>
    /// The divider character used to separate the header from the content.
    /// </summary>
    protected string _divider = " ";
    /// <summary>
    /// The main content of the screen.
    /// </summary>
    protected string _content = " ";

    /// <summary>
    /// Initializes a new instance of the <see cref="BaseGameScreen"/> class.
    /// </summary>
    public BaseGameScreen() { }

    /// <summary>
    /// Shows the screen by outputting the header, divider, and content to the console.
    /// </summary>
    public override void Show()
    {
        base.Show();
        System.Console.WriteLine(_header);
        for (int i = 0; i < System.Console.WindowWidth - 10; i++)
        {
            System.Console.Write(_divider);
        }
        System.Console.WriteLine();
        System.Console.SetCursorPosition((System.Console.WindowWidth - _content.Split("\n").Max(p => p.Length)) / 2, System.Console.CursorTop);
        foreach (var row in _content.Split("\n"))
        {
            System.Console.SetCursorPosition((System.Console.WindowWidth - _content.Split("\n").Max(p => p.Length)) / 2, System.Console.CursorTop);
            System.Console.WriteLine(row);
        }
        System.Console.SetCursorPosition(0, 0);
    }

    /// <summary>
    /// Updates the screen by hiding it and then showing it again.
    /// </summary>
    public virtual void Update()
    {
        Hide();
        Show();
    }
}
