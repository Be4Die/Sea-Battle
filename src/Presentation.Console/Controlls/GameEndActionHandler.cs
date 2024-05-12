using SeaBattle.Application;

namespace SeaBattle.PresentationConsole.Controlls;

internal class GameEndActionHandler : IGameEndActionHandler
{
    public bool Enabaled { get; set; }

    public event Action? OnRestartHandle;
    public event Action? OnQuietHandle;

    private readonly Input _input;

    public GameEndActionHandler(Input input)
    {
        _input = input;

        _input.OnKeyPress += OnKeyPressCallback;
    }

    private void OnKeyPressCallback(ConsoleKeyInfo key)
    {
        if (!Enabaled)
            return;

        if (key.Key == ConsoleKey.R)
        {
            OnRestartHandle?.Invoke();
        }
        else if (key.Key == ConsoleKey.Q)
        {
            OnQuietHandle?.Invoke();
        }
    }
}
