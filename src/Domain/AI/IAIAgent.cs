using SeaBattle.Domain.GameBoard;

namespace SeaBattle.Domain.AI;

/// <summary>
/// Represents an AI agent that can make moves and generate a game board.
/// </summary>
public interface IAIAgent
{
    /// <summary>
    /// Makes a move using the AI agent's strategy.
    /// </summary>
    public void MakeMove();

    /// <summary>
    /// Generates a new game board with the specified dimensions and places the given ships on it.
    /// </summary>
    /// <param name="width">The width of the board to generate.</param>
    /// <param name="height">The height of the board to generate.</param>
    /// <param name="shipsToPlace">The ships to place on the board.</param>
    /// <returns>A new <see cref="Board"/> object with the specified dimensions and ships placed.</returns>
    public Board GenerateBoard(uint width, uint height, Ship[] shipsToPlace);

    /// <summary>
    /// Fills the given board with the specified ships.
    /// </summary>
    /// <param name="board">The board to fill.</param>
    /// <param name="shipsToPlace">The ships to place on the board.</param>
    public void FillBoard(Board board, Ship[] shipsToPlace);
}
