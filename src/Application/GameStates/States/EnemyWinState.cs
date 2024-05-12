namespace SeaBattle.Application.GameStates.States;

public class EnemyWinState : GameEndState
{
    public EnemyWinState(IGameEndActionHandler restartHandler) : base(restartHandler) { }
}
