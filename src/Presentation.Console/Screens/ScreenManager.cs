using SeaBattle.Application.BoardBuilding;
using SeaBattle.Application.Contexts;
using SeaBattle.Application.GameStates;
using SeaBattle.Application.GameStates.States;
using SeaBattle.Domain.GameBoard;

namespace SeaBattle.PresentationConsole.Screens;

internal sealed class ScreenManager : IDisposable
{
    private Dictionary<Type, ScreenView> _screens = new ();
    private ScreenView? _lastScreen;
    private GameStateMachine _stateMachine;


    public ScreenManager(GameStateMachine stateMachine)
    {
        _screens[typeof(SetupState)] = new StartupScreen();
        _screens[typeof(BuildingBoardState)] = new BuildingScreen(
            GameContext.ResolveById<Board>(GameContext.PlayerBoardId),
            GameContext.ResolveSingle<PlayerBoardBuilder>());
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
