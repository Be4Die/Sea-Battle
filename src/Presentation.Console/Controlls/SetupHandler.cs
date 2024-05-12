using SeaBattle.Application;

namespace SeaBattle.Presentation.Console.Controlls;

/// <summary>
/// Handles game setup actions in a console application.
/// </summary>
internal class SetupHandler : IGameSetupHandler, IDisposable
{
    /// <summary>
    /// Event that is triggered when the game setup has ended.
    /// </summary>
    public event Action? OnSetupEnd;

    /// <summary>
    /// Gets or sets a value indicating whether the setup handler is enabled.
    /// </summary>
    public bool Enabaled { get; set; }


    private Input _input;

    /// <summary>
    /// Initializes a new instance of the <see cref="SetupHandler"/> class with the specified input handler.
    /// </summary>
    /// <param name="input">The input handler to use for key press events.</param>
    public SetupHandler(Input input)
    {
        _input = input;
        _input.OnKeyPress += OnKeyPressCallback;
    }

    /// <summary>
    /// Callback method that is invoked when a key press event occurs.
    /// </summary>
    /// <param name="obj">The key press event arguments.</param>
    private void OnKeyPressCallback(ConsoleKeyInfo obj)
    {
        if (!Enabaled)
            return;

        OnSetupEnd?.Invoke();
    }

    /// <summary>
    /// Releases the resources used by the <see cref="SetupHandler"/> class.
    /// </summary>
    public void Dispose()
    {
        _input.OnKeyPress -= OnKeyPressCallback;
    }
}
