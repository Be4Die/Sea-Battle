namespace SeaBattle.PresentationConsole.Screens;

internal class BaseGameScreen : ScreenView
{
    protected string _header = " ";
    protected string _divider = " ";
    protected string _content = " ";
    public BaseGameScreen() { }

    public override void Show()
    {
        base.Show();
        Console.WriteLine(_header);
        for (int i = 0; i < Console.WindowWidth-10; i++)
        {
            Console.Write(_divider);
        }
        Console.WriteLine();
        Console.SetCursorPosition((Console.WindowWidth - _content.Split("\n").Max(p => p.Length)) / 2, Console.CursorTop);
        foreach (var row in _content.Split("\n"))
        {
            Console.SetCursorPosition((Console.WindowWidth - _content.Split("\n").Max(p => p.Length)) / 2, Console.CursorTop);
            Console.WriteLine(row);    
        }
        Console.SetCursorPosition(0, 0);
    }

    public virtual void Update()
    {
        Hide();
        Show();
    }
}
