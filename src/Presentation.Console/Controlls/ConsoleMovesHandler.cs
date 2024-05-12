using SeaBattle.Application.PlayerMoves;
using SeaBattle.Domain.BoardNavigation;

namespace SeaBattle.PresentationConsole.Controlls;

/// <summary>
/// Handles player moves in a console application.
/// </summary>
internal class ConsoleMovesHandler : IPlayerMovesHandler, IDisposable
{
    /// <summary>
    /// Event that is triggered when a move action is pressed.
    /// </summary>
    public event Action<MoveDirection>? OnMovePress;

    /// <summary>
    /// Event that is triggered when a shoot action is pressed.
    /// </summary>
    public event Action? OnShootPress;

    // <summary>
    /// Gets or sets a value indicating whether the console moves handler is enabled.
    /// </summary>
    public bool Enabaled { get; set; }

    private Input _input;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleMovesHandler"/> class with the specified input handler.
    /// </summary>
    /// <param name="input">The input handler to use for key press events.</param>
    public ConsoleMovesHandler(Input input)
    {
        _input = input;
        _input.OnKeyPress += KeyPressCallback;
    }

    private void KeyPressCallback(ConsoleKeyInfo info)
    {
        if (!Enabaled)
            return;

        if (info.Key == ConsoleKey.Enter)
            OnShootPress?.Invoke();
        else if (info.Key == ConsoleKey.W || info.Key == ConsoleKey.UpArrow)
            OnMovePress?.Invoke(MoveDirection.Up);
        else if (info.Key == ConsoleKey.S || info.Key == ConsoleKey.DownArrow)
            OnMovePress?.Invoke(MoveDirection.Down);
        else if (info.Key == ConsoleKey.A || info.Key == ConsoleKey.LeftArrow)
            OnMovePress?.Invoke(MoveDirection.Left);
        else if (info.Key == ConsoleKey.D || info.Key == ConsoleKey.RightArrow)
            OnMovePress?.Invoke(MoveDirection.Right);
    }

    /// <summary>
    /// Releases the resources used by the <see cref="ConsoleMovesHandler"/> class.
    /// </summary>
    public void Dispose()
    {
        Enabaled = false;
        _input.OnKeyPress -= KeyPressCallback;
    }
}
