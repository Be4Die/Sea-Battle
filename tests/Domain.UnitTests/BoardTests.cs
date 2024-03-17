namespace Domain.UnitTests;

public class BoardTests
{
    [Fact]
    public void Constructor_FromCeils_InstantiatedProperly()
    {
        // Arrange
        var ceils = new Ceil[,]
        {
        { new(), new(), new() },
        { new(), new(), new() },
        { new(), new(), new() }
        };

        // Act
        var board = new Board(ceils);

        // Assert
        Assert.NotNull(board);
        Assert.Equal(ceils.GetLength(0), (int)board.Width);
        Assert.Equal(ceils.GetLength(1), (int)board.Height);
    }

    [Fact]
    public void Constructor_FromSize_InstantiatedProperly()
    {
        // Arrange 
        var board = new Board(5, 5);

        // Assert
        Assert.NotNull(board);
        Assert.Equal(5, (int)board.Width);
        Assert.Equal(5, (int)board.Height);
    }

    [Fact]
    public void CanShipPlaced_ShipCanBePlaced_ReturnTrue()
    {
        // Arrange
        var ceils = new Ceil[,]
        {
        { new(), new(), new() },
        { new(), new(), new() },
        { new(), new(), new() }
        };

        var ship = new Ship(Ship.Orientations.Horizontal, 2);

        // Act
        var board = new Board(ceils);
        var result = board.CanShipPlaced(ship, 0, 0);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanShipPlaced_ShipNearAnother_ReturnFalse()
    {
        // Arrange
        var ceils = new Ceil[,]
        {
        { new(), new(), new() },
        { new(), new(), new() },
        { new(), new(), new() }
        };

        var ship = new Ship(Ship.Orientations.Horizontal, 2);
        var board = new Board(ceils);

        // Act
        board.PlaceShip(ship, 0, 0);
        var result = board.CanShipPlaced(ship, 0, 1);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void RemoveShip_WithExistingShip_ReturnTrue()
    {
        // Arrange
        var ceils = new Ceil[,]
        {
        { new(), new(), new() },
        { new(), new(), new() },
        { new(), new(), new() }
        };

        var ship = new Ship(Ship.Orientations.Horizontal, 2);
        var board = new Board(ceils);

        // Act
        board.PlaceShip(ship, 0, 0);
        var result = board.RemoveShip(ship, 0, 0);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void PlaceShip_WithCorrectShip_ReturnTrue()
    {
        // Arrange
        var ceils = new Ceil[,]
        {
        { new(), new(), new() },
        { new(), new(), new() },
        { new(), new(), new() }
        };

        var ship = new Ship(Ship.Orientations.Horizontal, 2);
        var board = new Board(ceils);

        // Act
        var result = board.PlaceShip(ship, 0, 0);

        // Assert
        Assert.True(result);
    }

}
