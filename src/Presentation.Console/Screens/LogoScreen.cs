namespace SeaBattle.PresentationConsole.Screens;

internal class LogoScreen : ScreenView
{
    protected string _logoText = "";
    protected string _subText = "";
    protected int _spaces = 2;

    public LogoScreen()
    {
        
    }

    public LogoScreen(string logo, string sub = "", int spaces = 2)
    {
        _logoText = logo;
        _subText = sub;
        _spaces = spaces;
    }

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
