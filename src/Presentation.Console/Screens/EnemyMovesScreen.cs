using SeaBattle.Application.PlayerMoves;
using SeaBattle.Domain.GameBoard;

namespace SeaBattle.PresentationConsole.Screens;

internal class EnemyMovesScreen : PlayerMovesScreen
{
    public EnemyMovesScreen(Board playerBoard, Board enemyBoard, PlayerMovesController movesController) : base(playerBoard, enemyBoard, movesController) { }
}
