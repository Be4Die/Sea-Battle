namespace SeaBattle.Application.GameStates.States;

public class PlayerWinState : GameEndState
{
    public PlayerWinState(IGameEndActionHandler restartHandler) : base(restartHandler) { }
}
