using SeaBattle.Application.BoardBuilding;
using SeaBattle.Application.Contexts;
using SeaBattle.Application.GameStates;
using SeaBattle.Application.GameStates.States;
using SeaBattle.Application.PlayerMoves;
using SeaBattle.Domain.GameBoard;

namespace SeaBattle.PresentationConsole.Screens;

internal sealed class ScreenManager : IDisposable
{
    private Dictionary<Type, ScreenView> _screens = new ();
    public ScreenView? _lastScreen;
    private GameStateMachine _stateMachine;


    public ScreenManager(GameStateMachine stateMachine)
    {
        var playerBoard = GameContext.ResolveById<Board>(GameContext.PlayerBoardId);
        var enemyBoard = GameContext.ResolveById<Board>(GameContext.EnemyBoardId);
        _screens[typeof(SetupState)] = new StartupScreen();

        _screens[typeof(BuildingBoardState)] = new BuildingScreen(
            playerBoard,
            GameContext.ResolveSingle<PlayerBoardBuilder>());
        _screens[typeof(PlayerMoveState)] = new PlayerMovesScreen(
            playerBoard,
            enemyBoard,
            GameContext.ResolveSingle<PlayerMovesController>());
        /*_screens[typeof(EnemyMoveState)] = new EnemyMovesScreen(playerBoard,
            enemyBoard,
            GameContext.ResolveSingle<PlayerMovesController>());*/
        _screens[typeof(EnemyMoveState)] = new EmptyScreen();
        _screens[typeof(PlayerWinState)] = new WinScreen();
        _screens[typeof(EnemyWinState)] = new LoseScreen();
        _screens[typeof(RestartState)] = new EmptyScreen();
        _screens[typeof(ExitState)] = new EmptyScreen(); 

        _stateMachine = stateMachine;
        _stateMachine.OnStateChanged += OnStateChangedCallback;
    }

    public void Dispose()
    {
        _stateMachine.OnStateChanged -= OnStateChangedCallback;
    }

    private void OnStateChangedCallback(Type type)
    {
        if (!_screens.ContainsKey(type))
            throw new MissingScreenException(type);

        _lastScreen?.Hide();
        _lastScreen = _screens[type];
        _lastScreen.Show();
    }
}
