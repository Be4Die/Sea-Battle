using SeaBattle.Application.BoardBuilding;
using SeaBattle.Domain.BoardNavigation;

namespace SeaBattle.PresentationConsole.Controlls;

internal class ConsoleBoardBuilderHandler : IPlayerBoardBuilldHandler, IDisposable
{
    public bool Enabaled { get ; set; }

    public event Action? OnRotatePress;
    public event Action<MoveDirection>? OnMovePress;
    public event Action? OnPlacePress;

    private Input _input;

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

    public void Dispose()
    {
    }
}
