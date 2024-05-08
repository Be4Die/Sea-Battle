using SeaBattle.Application;
using SeaBattle.Application.BoardBuilding;
using SeaBattle.Application.Contexts;
using SeaBattle.Application.Cycle;
using SeaBattle.Application.GameStates;
using SeaBattle.Application.PlayerMoves;

using SeaBattle.PresentationConsole.Controlls;
using SeaBattle.PresentationConsole.Screens;
using System.Text;


Console.OutputEncoding = Encoding.UTF8;
var input = new Input();

GameContext.InjectSingle<IPlayerMovesHandler>(new ConsoleMovesHandler(input));
GameContext.InjectSingle<IPlayerBoardBuilldHandler>(new ConsoleBoardBuilderHandler(input));
GameContext.InjectSingle<IGameSetupHandler>(new SetupHandler(input));
GameContext.Initialize();

var cycle = GameContext.ResolveSingle<GameCycle>();
var screenManager = new ScreenManager(GameContext.ResolveSingle<GameStateMachine>());

cycle.Run();
input.Start();

while (true)
{
    await Task.Delay(35);
}