using SeaBattle.Domain.GameRules;

namespace SeaBattle.Domain.GameBoard;

public class BoardFactory
{
    public Board CreateBoardFromRules(GameRuleData data)
    {
        return new Board(data.BoardWidth, data.BoardHeight);
    }
}
