using SeaBattle.Application;

namespace SeaBattle.PresentationConsole.Controlls;

/// <summary>
/// Handles game end actions in a console application.
/// </summary>
internal class GameEndActionHandler : IGameEndActionHandler
{
    /// <summary>
    /// Event that is triggered when a restart action is handled.
    /// </summary>
    public event Action? OnRestartHandle;

    /// <summary>
    /// Event that is triggered when a quiet action is handled.
    /// </summary>
    public event Action? OnQuietHandle;


    /// <summary>
    /// Gets or sets a value indicating whether the game end action handler is enabled.
    /// </summary>
    public bool Enabaled { get; set; }

    private readonly Input _input;

    /// <summary>
    /// Initializes a new instance of the <see cref="GameEndActionHandler"/> class with the specified input handler.
    /// </summary>
    /// <param name="input">The input handler to use for key press events.</param>
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
            OnRestartHandle?.Invoke();
        else if (key.Key == ConsoleKey.Q)
            OnQuietHandle?.Invoke();
    }
}
