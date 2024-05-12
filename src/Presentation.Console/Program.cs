using SeaBattle.Application;
using SeaBattle.Application.BoardBuilding;
using SeaBattle.Application.Contexts;
using SeaBattle.Application.Cycle;
using SeaBattle.Application.GameStates;
using SeaBattle.Application.GameStates.States;
using SeaBattle.Application.PlayerMoves;

using SeaBattle.Presentation.Console.Controlls;
using SeaBattle.Presentation.Console.Screens;
using System.Text;

// Set the output encoding of the console to UTF-8 for proper display of special characters
Console.OutputEncoding = Encoding.UTF8;

// Create an instance of Input for handling user input
var input = new Input();

// Inject several handlers into the GameContext for game setup, player moves, board building, and game end actions
GameContext.InjectSingle<IGameEndActionHandler>(new GameEndActionHandler(input));
GameContext.InjectSingle<IPlayerMovesHandler>(new ConsoleMovesHandler(input));
GameContext.InjectSingle<IPlayerBoardBuilldHandler>(new ConsoleBoardBuilderHandler(input));
GameContext.InjectSingle<IGameSetupHandler>(new SetupHandler(input));

// Initialize the GameContext to set up the dependency injection container with necessary services
GameContext.Initialize();

// Resolve an instance of GameCycle and GameStateMachine from the GameContext
var cycle = GameContext.ResolveSingle<GameCycle>();
var gameStates = GameContext.ResolveSingle<GameStateMachine>();

// Create an instance of ScreenManager to manage the game screens
var screenManager = new ScreenManager(GameContext.ResolveSingle<GameStateMachine>());


// Start the game cycle
cycle.Run();

// Start the input processing
input.Start();

// Enter a loop that checks the current state of the game
while (gameStates.CurrentStateType != typeof(ExitState))
{
    // Wait for a short delay before checking the game state again
    await Task.Delay(35);
}

// Dispose of the input, screenManager, and GameContext when the game state is ExitState
input.Dispose();
screenManager.Dispose();
GameContext.Dispose();