using SeaBattle.Application.BoardBuilding;
using SeaBattle.Domain.BoardNavigation;

namespace SeaBattle.Presentation.Console.Controlls;

/// <summary>
/// Handles player board building actions in a console application.
/// </summary>  
internal class ConsoleBoardBuilderHandler : IPlayerBoardBuilldHandler, IDisposable
{
    /// <summary>
    /// Event that is triggered when a rotate action is pressed.
    /// </summary>
    public event Action? OnRotatePress;

    /// <summary>
    /// Event that is triggered when a move action is pressed.
    /// </summary>
    public event Action<MoveDirection>? OnMovePress;

    /// <summary>
    /// Event that is triggered when a place action is pressed.
    /// </summary>
    public event Action? OnPlacePress;


    /// <summary>
    /// Gets or sets a value indicating whether the console board builder handler is enabled.
    /// </summary>
    public bool Enabaled { get ; set; }

    private readonly Input _input;

    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleBoardBuilderHandler"/> class with the specified input handler.
    /// </summary>
    /// <param name="input">The input handler to use for key press events.</param>
    public ConsoleBoardBuilderHandler(Input input)
    {
        _input = input;

        _input.OnKeyPress += KeyPressCallback;
    }

    private void KeyPressCallback(ConsoleKeyInfo info)
    {
        if (!Enabaled)
            return;

        if (info.Key == ConsoleKey.Enter)
            OnPlacePress?.Invoke();
        if (info.Key == ConsoleKey.R)
            OnRotatePress?.Invoke();
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
    /// Releases the resources used by the <see cref="ConsoleBoardBuilderHandler"/> class.
    /// </summary>
    public void Dispose()
    {
        Enabaled = false;
        _input.OnKeyPress -= KeyPressCallback;
    }
}
