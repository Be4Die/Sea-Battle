using SeaBattle.Application;
namespace SeaBattle.PresentationConsole.Controlls;

internal class SetupHandler : IGameSetupHandler, IDisposable
{
    public bool Enabaled { get; set; }

    public event Action? OnSetupEnd;
    private Input _input;

    public SetupHandler(Input input)
    {
        _input = input;
        _input.OnKeyPress += OnKeyPressCallback;
    }

    private void OnKeyPressCallback(ConsoleKeyInfo obj)
    {
        if (!Enabaled)
            return;

        OnSetupEnd?.Invoke();
    }

    public void Dispose()
    {
        _input.OnKeyPress -= OnKeyPressCallback;
    }
}
