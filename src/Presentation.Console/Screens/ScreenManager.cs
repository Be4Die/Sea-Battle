using SeaBattle.Application.BoardBuilding;
using SeaBattle.Application.Contexts;
using SeaBattle.Application.GameStates;
using SeaBattle.Application.GameStates.States;
using SeaBattle.Application.PlayerMoves;
using SeaBattle.Domain.GameBoard;

namespace SeaBattle.Presentation.Console.Screens;

/// <summary>
/// Manages the screens for a console application based on the game state.
/// </summary>
internal sealed class ScreenManager : IDisposable
{
    private readonly Dictionary<Type, ScreenView> _screens = [];
    private readonly GameStateMachine _stateMachine;
    public ScreenView? _lastScreen;

    /// <summary>
    /// Initializes a new instance of the <see cref="ScreenManager"/> class with the specified game state machine.
    /// </summary>
    /// <param name="stateMachine">The game state machine to use for managing the screens.</param>
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
        _screens[typeof(EnemyMoveState)] = new EmptyScreen();
        _screens[typeof(PlayerWinState)] = new WinScreen();
        _screens[typeof(EnemyWinState)] = new LoseScreen();
        _screens[typeof(RestartState)] = new EmptyScreen();
        _screens[typeof(ExitState)] = new EmptyScreen(); 

        _stateMachine = stateMachine;
        _stateMachine.OnStateChanged += OnStateChangedCallback;
    }

    private void OnStateChangedCallback(Type type)
    {
        if (!_screens.TryGetValue(type, out var screen))
            throw new MissingScreenException(type);

        _lastScreen?.Hide();
        _lastScreen = screen;
        _lastScreen.Show();
    }

    /// <summary>
    /// Releases the resources used by the <see cref="ScreenManager"/> class.
    /// </summary>
    public void Dispose()
    {
        _stateMachine.OnStateChanged -= OnStateChangedCallback;
    }

}
