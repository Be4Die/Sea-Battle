using Presentation.Console.Resources;
using SeaBattle.Application.PlayerMoves;
using SeaBattle.Domain.GameBoard;
using System.Text;

namespace SeaBattle.Presentation.Console.Screens;

/// <summary>
/// Represents a screen that displays the player's moves in a console application.
/// </summary>
internal class PlayerMovesScreen : BaseGameScreen
{
    private readonly Board _playerBoard;
    private readonly Board _enemyBoard;
    private readonly PlayerMovesController _controller;

    /// <summary>
    /// Initializes a new instance of the <see cref="PlayerMovesScreen"/> class with the specified player and enemy boards and moves controller.
    /// </summary>
    /// <param name="playerBoard">The player's board.</param>
    /// <param name="enemyBoard">The enemy's board.</param>
    /// <param name="movesController">The moves controller.</param>
    public PlayerMovesScreen(Board playerBoard, Board enemyBoard, PlayerMovesController movesController)
    {
        _header = TextsRU.AttackEnemyTask.Replace("\\n", Environment.NewLine);
        _divider = Chars.BorderBottom;
        _playerBoard = playerBoard;
        _enemyBoard = enemyBoard;
        _controller = movesController;

        _content = EnemyBoardString() + "\n" + PlayerBoardString();

        _controller.OnMoved += ControllerOnMovedCallback;
    }

    private void ControllerOnMovedCallback(Domain.BoardNavigation.MoveDirection obj) => Update();

    /// <summary>
    /// Updates the screen content.
    /// </summary>
    public override void Update()
    {
        _content = EnemyBoardString() + "\n" + PlayerBoardString();
        base.Update();
    }

    /// <summary>
    /// Shows the screen.
    /// </summary>
    public override void Show()
    {
        _content = EnemyBoardString() + "\n" + PlayerBoardString();
        base.Show();
    }

    private string PlayerBoardString()
    {
        StringBuilder sb = new ();
        for (int i = 0; i < _playerBoard.Width * 2 + 1; i++)
        {
            sb.Append(Chars.BorderBottom);
        }
        sb.AppendLine();
        for (int y = 0; y < _playerBoard.Height; y++)
        {
            sb.Append(Chars.BorderLeft);
            for (int x = 0; x < _playerBoard.Width; x++)
            {
                var ceil = _playerBoard.GetCeil((uint)x, (uint)y);
                if (ceil != null && ceil.ContainShip)
                {
                    if (ceil.Ship != null && ceil.ShipSegmentIndex != null && ceil.Ship.CheckSegment((int)ceil.ShipSegmentIndex))
                    {
                        sb.Append(Chars.Ship);
                    }
                    else
                    {
                        sb.Append(Chars.HitedShip);
                    }
                }
                else if (ceil != null && ceil.IsHited)
                {
                    sb.Append(Chars.Hit);
                }
                else
                {
                    sb.Append(' ');
                }
                if (x != _playerBoard.Width - 1)
                    sb.Append(' ');
            }
            sb.Append(Chars.BorderRight);
            sb.AppendLine();
        }
        for (int i = 0; i < _playerBoard.Width * 2 + 1; i++)
        {
            sb.Append(Chars.BoarderTop);
        }
        sb.AppendLine();    

        return sb.ToString();
    }

    private string EnemyBoardString()
    {
        StringBuilder sb = new ();
        for (int i = 0; i < _enemyBoard.Width * 2 + 1; i++)
        {
            sb.Append(Chars.BorderBottom);
        }
        sb.AppendLine();
        for (int y = 0; y < _enemyBoard.Height; y++)
        {
            sb.Append(Chars.BorderLeft);
            for (int x = 0; x < _enemyBoard.Width; x++)
            {
                var ceil = _enemyBoard.GetCeil((uint)x, (uint)y);

                if (_controller.Cursor.X == x && _controller.Cursor.Y == y)
                {
                    sb.Append(Chars.Cursor);
                }
                else if (ceil != null && ceil.ContainShip)
                {
                    if (ceil.Ship != null && ceil.ShipSegmentIndex != null && ceil.Ship.CheckSegment((int)ceil.ShipSegmentIndex))
                    {
                        sb.Append(' ');
                    }
                    else
                    {
                        sb.Append(Chars.HitedShip);
                    }
                }
                else if (ceil != null && ceil.IsHited)
                {
                    sb.Append(Chars.Hit);
                }
                else
                {
                    sb.Append(' ');
                }
                if (x != _enemyBoard.Width - 1)
                    sb.Append(' ');
            }
            sb.Append(Chars.BorderRight);
            sb.AppendLine();
        }
        for (int i = 0; i < _enemyBoard.Width * 2 + 1; i++)
        {
            sb.Append(Chars.BoarderTop);
        }
        sb.AppendLine();

        return sb.ToString();
    }
}
