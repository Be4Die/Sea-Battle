# Sea Battle
An example of “Sea Battle” game written in c# using Clean Architecture pattern. Now there is a Presentation layer for console only.
The project was created as a part of course work at the university (1st year)

**TODO**
1. Create presentation layer using Unity engine (3D game)
2. Try create presentation layer using Monogame framework (2D Game)
## About the game
This is a classic naval combat game. 
- You play against the computer (now there are 2 difficulties to change them can only through the code)
- Ships can be placed both vertically and horizontally
- The playing field is 10x10, standard set of ships. 
- Ships cannot be placed next to each other. 

There will be several screens in the game
- Start of the game, you need to press any button to continue.
- Ship placement, press WASD or arrow keys to move ships, press Enter to place, press R to rotate ship
- Attack the enemy, move the cursor with WASD or arrow keys, attack with Enter.
- Result of the game, when one of the players destroys all the ships, the game will end and you will see the result. After that you will be prompted to either end the game and exit or try again.
## Create solution via dotnet CLI
### Create solution and projects
```
dotnet new sln --name SeaBattle
dotnet new classlib -n Domain -o ./src/Domain/
dotnet new classlib -n Application -o ./src/Application/
rm  ./src/Domain/Class1.cs
rm  ./src/Application/Class1.cs
mv ./src/Presentation.Console/Program.cs ./src/Presentation.Console/tProgram.cs
dotnet new console -n Presentation.Console -o ./src/Presentation.Console/
mv ./src/Presentation.Console/tProgram.cs ./src/Presentation.Console/Program.cs
```
###  Add references to projects
```
cd ./src/Application/
dotnet add reference ../Domain/Domain.csproj
cd ../Presentation.Console/
dotnet add reference ../Application/Application.csproj
dotnet add reference ../Domain/Domain.csproj
cd ../..	
```
### Add projects to solution
```
dotnet sln  add ./src/Domain/Domain.csproj
dotnet sln  add ./src/Application/Application.csproj
dotnet sln  add ./src/Presentation.Console/Presentation.Console.csproj
```
make sure everything's okay.
```
dotner run --project ./src/Presentation.Console/Presentation.Console.csproj
```
If all is well, the game will launch in the console.
### * Create unit tests
```
dotnet new xunit -n Domain.UnitTests -o ./tests/Domain.UnitTests/
rm ./tests/Domain.UnitTests/UnitTest1.cs
dotnet new xunit -n Application.UnitTests -o ./tests/Application.UnitTests/
rm ./tests/Application.UnitTests/UnitTest1.cs
cd ./tests/Domain.UnitTests/
dotnet add reference ../../src/Domain/Domain.csproj
cd ../Application.UnitTests/
dotnet add reference ../../src/Domain/Domain.csproj
dotnet add reference ../../src/Application/Application.csproj
cd ../..
dotnet sln add ./tests/Domain.UnitTests/Domain.UnitTests.csproj
dotnet sln add ./tests/Application.UnitTests/Application.UnitTests.csproj
```
make sure everything's okay.
```
dotnet test
```
If the tests run, you've done the right thing.
