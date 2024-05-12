using SeaBattle.Application.BoardBuilding;
using SeaBattle.Application.GameStates.States;
using SeaBattle.Application.PlayerMoves;
using SeaBattle.Domain.GameBoard;
using SeaBattle.Domain.StateMachinePattern;

namespace SeaBattle.Application.Cycle;

public sealed class GameCycle : IDisposable
{
    private readonly IStateMachine _gameStates;
    private readonly PlayerBoardBuilder _boardBuilder;
    private readonly PlayerMovesController _movesController;
    private readonly Board _playerBoard;
    private readonly Board _enemyBoard;
    private readonly IGameSetupHandler _gameSetup;
    private readonly IGameEndActionHandler _gameEndActionHandler;

    public GameCycle(IStateMachine gameStates, IGameSetupHandler gameSetup, PlayerBoardBuilder playerBoardBuilder,
        PlayerMovesController movesController, Board playerBoard, Board enemyBoard,
        IGameEndActionHandler gameEndActionHandler)
    {
        _gameStates = gameStates;
        _boardBuilder = playerBoardBuilder;
        _movesController = movesController;
        _playerBoard = playerBoard;
        _enemyBoard = enemyBoard;
        _gameSetup = gameSetup;
        _gameEndActionHandler = gameEndActionHandler;

        _gameSetup.OnSetupEnd += OnGameSetupEndCallback;
        _boardBuilder.OnBuildCompleted += OnBuildCompletedCallback;
        _movesController.OnMoveCompleted += OnPlayerMoveCompleted;
        _gameEndActionHandler.OnRestartHandle += OnRestartHandleCallback;
        _gameEndActionHandler.OnQuietHandle += OnQuietHandleCallback;
        _enemyBoard.OnHited += OnEnemyBoardHited;
        _playerBoard.OnHited += OnPlayerBoardHited;
    }

    private void OnPlayerBoardHited(uint arg1, uint arg2, bool arg3)
    {
        if (_playerBoard.CheckAllShipsDestroiedCondition())
        {
            _gameStates.Change<EnemyWinState>();
        }
    }

    private void OnEnemyBoardHited(uint arg1, uint arg2, bool arg3)
    {
        if (_enemyBoard.CheckAllShipsDestroiedCondition())
        {
            _gameStates.Change<PlayerWinState>();
        }
    }

    private void OnQuietHandleCallback()
    {

        _gameStates.Change<ExitState>();
    }

    private void OnRestartHandleCallback()
    {
        _gameStates.Change<RestartState>();
        _gameStates.Change<BuildingBoardState>();
    }

    public void Run()
    {
        _gameStates.Change<SetupState>();
    }

    private void OnGameSetupEndCallback()
    {
        _gameStates.Change<BuildingBoardState>();
    }

    private void OnPlayerMoveCompleted()
    {
        if (_enemyBoard.CheckAllShipsDestroiedCondition())
        {
            _gameStates.Change<PlayerWinState>();
            return;
        }

        _gameStates.Change<EnemyMoveState>();

        if (_playerBoard.CheckAllShipsDestroiedCondition())
        {
            _gameStates.Change<EnemyWinState>();
            return;
        }

        _gameStates.Change<PlayerMoveState>();
    }
    private void OnBuildCompletedCallback(Board board)
    {
        _gameStates.Change<PlayerMoveState>();
    }

    public void Dispose()
    {
        _gameSetup.OnSetupEnd -= OnGameSetupEndCallback;
        _boardBuilder.OnBuildCompleted -= OnBuildCompletedCallback;
        _movesController.OnMoveCompleted -= OnPlayerMoveCompleted;
        _gameEndActionHandler.OnRestartHandle -= OnRestartHandleCallback;
    }
}
