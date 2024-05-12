using SeaBattle.Domain.AI;
using SeaBattle.Domain.GameRules;

namespace SeaBattle.Domain.GameBoard;

public class BoardFactory
{
    public Board CreateBoardFromRules(GameRuleData data)
    {
        return new Board(data.BoardWidth, data.BoardHeight);
    }

    public Board CreateEnemyBoard(GameRuleData data, IAIAgent agent) => agent.GenerateBoard(data.BoardWidth, data.BoardHeight,
        new ShipFactory().CreateShipsFromGameRule(data));
}
