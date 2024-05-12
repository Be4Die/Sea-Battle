using Presentation.Console.Resources;
using SeaBattle.Application.PlayerMoves;
using SeaBattle.Domain.GameBoard;
using System.Diagnostics;
using System.Text;

namespace SeaBattle.PresentationConsole.Screens;


internal class PlayerMovesScreen : BaseGameScreen
{
    private readonly Board _playerBoard;
    private readonly Board _enemyBoard;
    private readonly PlayerMovesController _controller;

    public PlayerMovesScreen(Board playerBoard, Board enemyBoard, PlayerMovesController movesController)
    {
        _header = TextsRU.AttackEnemyTask.Replace("\\n", Environment.NewLine);
        _divider = Chars.BorderBottom;
        _playerBoard = playerBoard;
        _enemyBoard = enemyBoard;
        _controller = movesController;


        _content = CombineBoards(PlayerBoardString(), EnemyBoardString());

        _controller.OnMoved += ControllerOnMovedCallback;
    }

    private void ControllerOnMovedCallback(Domain.BoardNavigation.MoveDirection obj) => Update();

    public override void Update()
    {
        //_content = CombineBoards(EnemyBoardString(), PlayerBoardString());
        _content = EnemyBoardString() + "\n" + PlayerBoardString();
        base.Update();
    }

    public override void Show()
    {
        //_content = CombineBoards(EnemyBoardString(), PlayerBoardString());
        _content = EnemyBoardString() + "\n" + PlayerBoardString();
        base.Show();
    }

    private string CombineBoards(string left, string right)
    {
        string[] linesLeft = left.Split('\n'); 
        string[] linesRight = right.Split('\n'); 

        int maxLengthLeft = GetMaxLength(linesLeft); 
        int maxLengthRight = GetMaxLength(linesRight); 

        int maxLength = Math.Max(maxLengthLeft, maxLengthRight);

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < linesLeft.Length; i++)
        {
            sb.AppendLine(linesLeft[i].PadRight(maxLength) + "     " + linesRight[i].PadLeft(maxLength));
        }
        sb.AppendLine();
        return sb.ToString();
    }

    private string PlayerBoardString()
    {
        StringBuilder sb = new StringBuilder();
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
        StringBuilder sb = new StringBuilder();
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


    private int GetMaxLength(string[] lines)
    {
        int maxLength = 0;
        foreach (string line in lines)
        {
            if (line.Length > maxLength)
            {
                maxLength = line.Length;
            }
        }
        return maxLength;
    }
}
