﻿using Presentation.Console.Resources;
using SeaBattle.Application.BoardBuilding;
using SeaBattle.Domain.GameBoard;
using System.Text;

namespace SeaBattle.Presentation.Console.Screens;

/// <summary>
/// Represents a screen that displays the board building process in a console application.
/// </summary>
internal class BuildingScreen : BaseGameScreen
{
    private readonly PlayerBoardBuilder _boardBuilder;
    private readonly Board _board;

    /// <summary>
    /// Initializes a new instance of the <see cref="BuildingScreen"/> class with the specified board and board builder.
    /// </summary>
    /// <param name="board">The board to be built.</param>
    /// <param name="builder">The board builder.</param>
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

    /// <summary>
    /// Updates the screen content.
    /// </summary>
    public override void Update()
    {
        _content = BoardToString();
        base.Update();
    }

    /// <summary>
    /// Shows the screen.
    /// </summary>
    public override void Show()
    {
        _content = BoardToString();
        base.Show();
    }

    private string BoardToString()
    {
        // Оставь надежду всяк сюда входящий
        StringBuilder sb = new();
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
                        if (y <= _boardBuilder.CursorData.Y + _boardBuilder.CursorData.HoldingShip.Length - 1 && y >= _boardBuilder.CursorData.Y
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
