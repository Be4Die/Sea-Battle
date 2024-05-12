using System.Diagnostics;

namespace SeaBattle.PresentationConsole.Controlls;

public sealed class Input : IDisposable
{
    public event Action<ConsoleKeyInfo>? OnKeyPress;

    private readonly int _delay = 50;
    private CancellationTokenSource _cts = new CancellationTokenSource();
    private Task? _inputTask;

    public Input()
    {
        OnKeyPress += LogKey;
    }

    public void Start()
    {
        _inputTask = Task.Run(HandleKeys, _cts.Token);
    }

    public void Stop()
    {
        _cts.Cancel();
    }

    public void Dispose()
    {
        OnKeyPress -= LogKey;
        _cts.Cancel();
        _inputTask?.Wait();
        _cts.Dispose();
    }

    private async Task HandleKeys()
    {
        ConsoleKeyInfo keyInfo;

        while (!_cts.IsCancellationRequested)
        {
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                OnKeyPress?.Invoke(keyInfo);
            }

            await Task.Delay(_delay).ConfigureAwait(false); ;
        }
    }

    private void LogKey(ConsoleKeyInfo key) => Debug.WriteLine($"[{nameof(Input)}] handle key: {key.Key}");
}
