using System.Diagnostics;

namespace SeaBattle.Presentation.Console.Controlls;

/// <summary>
/// Handles console input for a console application.
/// </summary>
public sealed class Input : IDisposable
{
    /// <summary>
    /// Event that is triggered when a key press occurs.
    /// </summary>
    public event Action<ConsoleKeyInfo>? OnKeyPress;

    private readonly int _delay = 50;
    private readonly CancellationTokenSource _cts = new ();
    private Task? _inputTask;

    /// <summary>
    /// Initializes a new instance of the <see cref="Input"/> class.
    /// </summary>
    public Input()
    {
        OnKeyPress += LogKey;
    }

    /// <summary>
    /// Starts handling console input.
    /// </summary>
    public void Start() => _inputTask = Task.Run(HandleKeys, _cts.Token);

    /// <summary>
    /// Stops handling console input.
    /// </summary>
    public void Stop() =>  _cts.Cancel();

    /// <summary>
    /// Asynchronously handles key presses.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    private async Task HandleKeys()
    {
        ConsoleKeyInfo keyInfo;

        while (!_cts.IsCancellationRequested)
        {
            if (System.Console.KeyAvailable)
            {
                keyInfo = System.Console.ReadKey(true);
                OnKeyPress?.Invoke(keyInfo);
            }

            await Task.Delay(_delay).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Logs the key press to the debug output.
    /// </summary>
    /// <param name="key">The key that was pressed.</param>
    private void LogKey(ConsoleKeyInfo key) => Debug.WriteLine($"[{nameof(Input)}] handle key: {key.Key}");

    /// <summary>
    /// Releases the resources used by the <see cref="Input"/> class.
    /// </summary>
    public void Dispose()
    {
        OnKeyPress -= LogKey;
        _cts.Cancel();
        _inputTask?.Wait();
        _cts.Dispose();
    }
}
