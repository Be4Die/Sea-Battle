using Presentation.Console.Resources;
using SeaBattle.Application.BoardBuilding;
using SeaBattle.Domain.GameBoard;
using System.Text;


namespace SeaBattle.PresentationConsole.Screens;

internal class BuildingScreen : BaseGameScreen
{
    private readonly PlayerBoardBuilder _boardBuilder;
    private readonly Board _board;
    public BuildingScreen(Board board, PlayerBoardBuilder builder)
    {
        _boardBuilder = builder;
        _board = board;
        _header = TextsRU.PlaceShipsTask.Replace("\\n", "\n").Replace("\\r", "\r");
        _divider = Chars.BorderBottom;
        _content = BoardToString();
        _boardBuilder.OnMoved += BuilderOnMovedCallback;
        _boardBuilder.OnShipChanged += BuilderOnShipChangedCallback;
        _boardBuilder.OnPlaced += BuilderOnPlacedCallback;
        _boardBuilder.OnRotated += BuilderOnRotated;
    }

    private void BuilderOnRotated() => Update();

    private void BuilderOnPlacedCallback((uint, uint) obj) => Update();

    private void BuilderOnShipChangedCallback(Domain.Ship? obj) => Update();

    private void BuilderOnMovedCallback(Domain.BoardNavigation.MoveDirection direction) => Update();

    public override void Update()
    {
        _content = BoardToString();
        base.Update();
    }

    public string BoardToString()
    {
        // Оставь надежду всяк сюда входящий
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < _board.Width * 2 + 1; i++)
        {
            sb.Append(Chars.BorderBottom);
        }
        sb.AppendLine();
        for (int y = 0; y < _board.Height; y++)
        {
            sb.Append(Chars.BorderLeft);
            for (int x = 0; x < _board.Width; x++)
            {
                var ceil = _board.GetCeil((uint)x, (uint)y);
                if (_boardBuilder.CursorData.X == x && _boardBuilder.CursorData.Y == y)
                {
                    sb.Append(Chars.Cursor);
                }
                else if (_boardBuilder.CursorData.HoldingShip != null && _boardBuilder.CursorData.HoldingShip.Length != 1)
                {
                    if (_boardBuilder.CursorData.HoldingShip.Orientation == Domain.Ship.Orientations.Vertical)
                    {
                        if (y <= _boardBuilder.CursorData.Y +_boardBuilder.CursorData.HoldingShip.Length - 1 && y >= _boardBuilder.CursorData.Y 
                            && _boardBuilder.CursorData.X == x)
                        {
                            sb.Append(Chars.Cursor);
                        }
                        else if (ceil != null && ceil.ContainShip)
                        {
                            sb.Append(Chars.Ship);
                        }
                        else
                        {
                            sb.Append(' ');
                        }
                    }
                    else
                    {
                        if (x <= _boardBuilder.CursorData.X + _boardBuilder.CursorData.HoldingShip.Length - 1 && x >= _boardBuilder.CursorData.X 
                            && _boardBuilder.CursorData.Y == y)
                        {
                            sb.Append(Chars.Cursor);
                        }
                        else if (ceil != null && ceil.ContainShip)
                        {
                            sb.Append(Chars.Ship);
                        }
                        else
                        {
                            sb.Append(' ');
                        }
                    }
                }
                else if (ceil != null && ceil.ContainShip)
                {
                    sb.Append(Chars.Ship);
                }
                else
                {
                    sb.Append(' ');
                }
                if (x != _board.Width - 1)
                    sb.Append(' ');
            }
            sb.Append(Chars.BorderRight);
            sb.AppendLine();
        }
        for (int i = 0; i < _board.Width * 2 + 1; i++)
        {
            sb.Append(Chars.BoarderTop);
        }

        return sb.ToString();
    }
}
