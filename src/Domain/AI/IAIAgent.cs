using SeaBattle.Domain.GameBoard;

namespace SeaBattle.Domain.AI;

public interface IAIAgent
{
    public void MakeMove();
    public Board GenerateBoard(uint width, uint height, Ship[] shipsToPlace);
    public void FillBoard(Board board, Ship[] shipsToPlace);
}
