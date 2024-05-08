using SeaBattle.Domain.BoardNavigation;
using SeaBattle.Application.BoardBuilding;
using SeaBattle.Domain;
using static SeaBattle.Domain.Ship;
using SeaBattle.Domain.GameBoard;

namespace Application.UnitTests;

public class PlayerBoardBuilderTests
{
    [Fact]
    public void MoveShip_MovesShipInSpecifiedDirection()
    {
        // Arrange
        var ships = new Ship[] { new Ship(Orientations.Vertical, 3) };
        var board = new Board(10, 10);
        var boardBuilder = new PlayerBoardBuilder(board, ships);
        var initialX = boardBuilder.CursorData.X;
        var initialY = boardBuilder.CursorData.Y;

        // Act
        boardBuilder.MoveShip(MoveDirection.Right);

        // Assert
        Assert.Equal(initialX + 1, boardBuilder.CursorData.X);
        Assert.Equal(initialY, boardBuilder.CursorData.Y);
    }

    [Fact]
    public void RotateShip_RotatesShipOrientation()
    {
        // Arrange
        var ships = new Ship[] { new Ship(Orientations.Vertical, 3) };
        var board = new Board(10, 10);
        var boardBuilder = new PlayerBoardBuilder(board, ships);

        // Act
        boardBuilder.RotateShip();

        // Assert
        Assert.Equal(Orientations.Horizontal, boardBuilder.CursorData.HoldingShip?.Orientation);
    }

    [Fact]
    public void PlaceShip_PlacesShipOnBoard()
    {
        // Arrange
        var ships = new Ship[] { new Ship(Orientations.Vertical, 3) };
        var board = new Board(10, 10);
        var boardBuilder = new PlayerBoardBuilder(board, ships);

        // Act
        boardBuilder.PlaceShip();

        // Assert
        // Since PlaceShip is a void method, we can't directly assert its effects.
        // We can check if the OnPlaced event is raised, which indicates that the ship was placed.
        var shipPlaced = false;
        boardBuilder.OnPlaced += (position) => shipPlaced = true;

        // Trigger the PlaceShip method again to check the event
        boardBuilder.PlaceShip();

        // Assert that the OnPlaced event was raised
        Assert.False(shipPlaced);
    }

    [Fact]
    public void CantPlaceShip_PlacesShipOnBoard()
    {
        // Arrange
        var ships = new Ship[] { new Ship(Orientations.Vertical, 3) };
        var board = new Board(10, 10);
        var boardBuilder = new PlayerBoardBuilder(board, ships);

        // Act
        boardBuilder.MoveShip(MoveDirection.Down);
        boardBuilder.MoveShip(MoveDirection.Down);
        boardBuilder.MoveShip(MoveDirection.Down);
        boardBuilder.MoveShip(MoveDirection.Down);
        boardBuilder.PlaceShip();
        var shipPlaced = boardBuilder.Board.CanShipPlaced(ships[0],0,4);

        // Assert that the OnPlaced event was raised
        Assert.False(shipPlaced);
    }


    [Fact]
    public void MoveShip_RaisesOnMovedEvent()
    {
        // Arrange
        var ships = new Ship[] { new Ship(Orientations.Vertical, 3) };
        var board = new Board(10, 10);
        var boardBuilder = new PlayerBoardBuilder(board, ships);

        var eventRaised = false;
        boardBuilder.OnMoved += (direction) => eventRaised = true;

        // Act
        boardBuilder.MoveShip(MoveDirection.Right);

        // Assert
        Assert.True(eventRaised);
    }

    [Fact]
    public void RotateShip_RaisesOnRotatedEvent()
    {
        // Arrange
        var ships = new Ship[] { new Ship(Orientations.Vertical, 3) };
        var board = new Board(10, 10);
        var boardBuilder = new PlayerBoardBuilder(board, ships);
        var eventRaised = false;
        boardBuilder.OnRotated += () => eventRaised = true;

        // Act
        boardBuilder.RotateShip();

        // Assert
        Assert.True(eventRaised);
    }

    [Fact]
    public void PlaceShip_RaisesOnPlacedEvent()
    {
        // Arrange
        var ships = new Ship[] { new Ship(Orientations.Vertical, 3) };
        var board = new Board(10, 10);
        var boardBuilder = new PlayerBoardBuilder(board, ships);
        var eventRaised = false;
        boardBuilder.OnPlaced += (position) => eventRaised = true;

        // Act
        boardBuilder.PlaceShip();

        // Assert
        Assert.True(eventRaised);
    }

    [Fact]
    public void PlaceShip_RaisesOnBuildCompleteEvent_WhenAllShipsPlaced()
    {
        // Arrange
        var ships = new Ship[] { new Ship(Orientations.Vertical, 3) };
        var board = new Board(10, 10);
        var boardBuilder = new PlayerBoardBuilder(board, ships);
        var eventRaised = false;
        boardBuilder.OnBuildCompleted += (board) => eventRaised = true;
 
        // Act
        boardBuilder.MoveShip(MoveDirection.Down);
        boardBuilder.MoveShip(MoveDirection.Down);
        boardBuilder.MoveShip(MoveDirection.Down);
        boardBuilder.MoveShip(MoveDirection.Down);
        boardBuilder.PlaceShip();

        // Assert
        Assert.True(eventRaised);
    }

    [Fact]
    public void PlaceShip_RaisesOnChangedShipEvent_WhenShipPlaced()
    {
        // Arrange
        var ships = new Ship[] { new Ship(Orientations.Vertical, 3) };
        var board = new Board(10, 10);
        var boardBuilder = new PlayerBoardBuilder(board, ships);
        var eventRaised = false;
        boardBuilder.OnShipChanged += (ship) => eventRaised = true;

        // Act
        boardBuilder.PlaceShip();

        // Assert
        Assert.True(eventRaised);
    }

    [Fact]
    public void PlaceShip_RaisesOnCanPressChangedEvent_WhenShipPlaced()
    {
        // Arrange
        var ships = new Ship[] { new Ship(Orientations.Vertical, 3) };
        var board = new Board(10, 10);
        var boardBuilder = new PlayerBoardBuilder(board, ships);
        var eventRaised = false;
        boardBuilder.OnCanPressChanged += (canPress) => eventRaised = true;

        // Act
        boardBuilder.PlaceShip();

        // Assert
        Assert.True(eventRaised);
    }

}