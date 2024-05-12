using System.Diagnostics;
using SeaBattle.Application.Cycle;
using SeaBattle.Application.BoardBuilding;
using SeaBattle.Application.GameStates;
using SeaBattle.Application.PlayerMoves;
using SeaBattle.Domain;
using SeaBattle.Domain.GameBoard;
using SeaBattle.Domain.GameRules;
using SeaBattle.Domain.AI;

namespace SeaBattle.Application.Contexts;

public static class GameContext
{
    public static readonly string PlayerBoardId = "Player";
    public static readonly string EnemyBoardId = "Enemy";

    private static Dictionary<Type, object> _singleDIContainer = new ();
    private static Dictionary<string, object> _idDIContainer = new ();

    public static void InjectSingle<T>(T instance)
    {
        if (instance == null)
            throw new NullReferenceException();

        if (_singleDIContainer.ContainsKey(typeof(T)))
            throw new InjectDuplicateSingleInstanceException(typeof(T));

        _singleDIContainer[typeof(T)] = instance;
        Debug.WriteLine($"[{nameof(GameContext)}] {nameof(GameContext.InjectSingle)} <{typeof(T).Name}>");
    }

    public static T ResolveSingle<T>()
    {
        if (!_singleDIContainer.ContainsKey(typeof(T)))
            throw new ResolveMissingDependencyException(typeof(T));

        return (T)_singleDIContainer[typeof(T)];
    }

    public static void InjectById<T>(T instance, string id)
    {
        if (instance == null)
            throw new NullReferenceException();

        if (_idDIContainer.ContainsKey(id))
            throw new InjectDuplicateSingleInstanceException(typeof(T));

        _idDIContainer[id] = instance;
        Debug.WriteLine($"[{nameof(GameContext)}] {nameof(GameContext.InjectById)} <{typeof(T).Name}> id:{id}");

    }

    public static T ResolveById<T>(string id)
    {
        if (!_idDIContainer.ContainsKey(id))
            throw new ResolveMissingDependencyException(typeof(T));

        return (T)_idDIContainer[id];
    }

    public static void Initialize()
    {
        var gameRule = new GameRuleFactory().CreateClassicData();
        InjectSingle<GameRuleData>(gameRule);
        var boardFactory = new BoardFactory();
        var playerBoard = boardFactory.CreateBoardFromRules(gameRule);
        InjectById<Board>(playerBoard, PlayerBoardId);
        var agent = AIAgentFactory.CreateAgent(gameRule.DifficultyLevel, playerBoard);
        InjectSingle<IAIAgent>(agent);
        var enemyBoard = boardFactory.CreateEnemyBoard(gameRule, agent);
        InjectById<Board>(enemyBoard, EnemyBoardId);

        InjectSingle<Ship[]>(new ShipFactory().CreateShipsFromGameRule(gameRule));

        InjectSingle<PlayerMovesController>(new PlayerMovesControllerFactory().Create());
        InjectSingle<PlayerBoardBuilder>(new PlayerBoardBuilderFactory().Create());

        InjectSingle<GameStateMachine>(new GameStateMachineFactory().Create());
        InjectSingle<GameCycle>(new GameCycleFactory().Create());

        Debug.WriteLine($"[{nameof(GameContext)}] {nameof(GameContext.Initialize)}");
    }

    public static void Dispose()
    {
        foreach (var item in _singleDIContainer)
        {
            var disposible = item.Value as IDisposable;
            if (disposible != null)
                disposible.Dispose();
        }

        foreach (var item in _idDIContainer)
        {
            var disposible = item.Value as IDisposable;
            if (disposible != null)
                disposible.Dispose();
        }

        Debug.WriteLine($"[{nameof(GameContext)}] {nameof(GameContext.Dispose)}");
    }
}
