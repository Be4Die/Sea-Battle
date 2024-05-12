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
    /// <summary>
    /// The identifier for the player's board.
    /// </summary>
    public static readonly string PlayerBoardId = "Player";

    /// <summary>
    /// The identifier for the enemy's board.
    /// </summary>
    public static readonly string EnemyBoardId = "Enemy";

    private readonly static Dictionary<Type, object> _singleDIContainer = [];
    private readonly static Dictionary<string, object> _idDIContainer = [];

    /// <summary>
    /// Injects a single instance of a type into the dependency injection container.
    /// </summary>
    /// <typeparam name="T">The type of the instance to inject.</typeparam>
    /// <param name="instance">The instance to inject.</param>
    /// <exception cref="NullReferenceException">Thrown if the instance is null.</exception>
    /// <exception cref="InjectDuplicateSingleInstanceException">Thrown if an instance of the same type is already injected.</exception>
    public static void InjectSingle<T>(T instance)
    {
        if (instance == null)
            throw new NullReferenceException();

        if (_singleDIContainer.ContainsKey(typeof(T)))
            throw new InjectDuplicateSingleInstanceException(typeof(T));

        _singleDIContainer[typeof(T)] = instance;
        Debug.WriteLine($"[{nameof(GameContext)}] {nameof(GameContext.InjectSingle)} <{typeof(T).Name}>");
    }

    /// <summary>
    /// Resolves a single instance of a type from the dependency injection container.
    /// </summary>
    /// <typeparam name="T">The type of the instance to resolve.</typeparam>
    /// <returns>The resolved instance.</returns>
    /// <exception cref="ResolveMissingDependencyException">Thrown if the instance is not found in the container.</exception>
    public static T ResolveSingle<T>()
    {
        if (!_singleDIContainer.ContainsKey(typeof(T)))
            throw new ResolveMissingDependencyException(typeof(T));

        return (T)_singleDIContainer[typeof(T)];
    }

    /// <summary>
    /// Injects an instance of a type into the dependency injection container with a specific identifier.
    /// </summary>
    /// <typeparam name="T">The type of the instance to inject.</typeparam>
    /// <param name="instance">The instance to inject.</param>
    /// <param name="id">The identifier for the instance.</param>
    /// <exception cref="NullReferenceException">Thrown if the instance is null.</exception>
    /// <exception cref="InjectDuplicateSingleInstanceException">Thrown if an instance with the same identifier is already injected.</exception>
    public static void InjectById<T>(T instance, string id)
    {
        if (instance == null)
            throw new NullReferenceException();

        if (_idDIContainer.ContainsKey(id))
            throw new InjectDuplicateSingleInstanceException(typeof(T));

        _idDIContainer[id] = instance;
        Debug.WriteLine($"[{nameof(GameContext)}] {nameof(GameContext.InjectById)} <{typeof(T).Name}> id:{id}");

    }

    /// <summary>
    /// Resolves an instance of a type from the dependency injection container by its identifier.
    /// </summary>
    /// <typeparam name="T">The type of the instance to resolve.</typeparam>
    /// <param name="id">The identifier of the instance.</param>
    /// <returns>The resolved instance.</returns>
    /// <exception cref="ResolveMissingDependencyException">Thrown if the instance is not found in the container.</exception>
    public static T ResolveById<T>(string id)
    {
        if (!_idDIContainer.TryGetValue(id, out var instance))
            throw new ResolveMissingDependencyException(typeof(T));

        return (T)instance;
    }

    /// <summary>
    /// Initializes the game context by injecting game components into the dependency injection container.
    /// </summary>
    public static void Initialize()
    {
        var gameRule = GameRuleFactory.CreateClassicData();
        InjectSingle<GameRuleData>(gameRule);
        var playerBoard = BoardFactory.CreateBoardFromRules(gameRule);
        InjectById<Board>(playerBoard, PlayerBoardId);
        var agent = AIAgentFactory.CreateAgent(gameRule.DifficultyLevel, playerBoard);
        InjectSingle<IAIAgent>(agent);
        var enemyBoard = BoardFactory.CreateEnemyBoard(gameRule, agent);
        InjectById<Board>(enemyBoard, EnemyBoardId);

        InjectSingle<Ship[]>(ShipFactory.CreateShipsFromGameRule(gameRule));

        InjectSingle<PlayerMovesController>(PlayerMovesControllerFactory.Create());
        InjectSingle<PlayerBoardBuilder>(PlayerBoardBuilderFactory.Create());

        InjectSingle<GameStateMachine>(GameStateMachineFactory.Create());
        InjectSingle<GameCycle>(GameCycleFactory.Create());

        Debug.WriteLine($"[{nameof(GameContext)}] {nameof(GameContext.Initialize)}");
    }


    /// <summary>
    /// Disposes of all disposable instances in the dependency injection container.
    /// </summary>
    public static void Dispose()
    {
        foreach (var item in _singleDIContainer)
        {
            var disposible = item.Value as IDisposable;
            disposible?.Dispose();
        }

        foreach (var item in _idDIContainer)
        {
            var disposible = item.Value as IDisposable;
            disposible?.Dispose();
        }

        Debug.WriteLine($"[{nameof(GameContext)}] {nameof(GameContext.Dispose)}");
    }
}
