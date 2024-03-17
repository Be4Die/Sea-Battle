namespace Domain.UnitTests;

public class CeilTests
{
    [Fact]
    public void Constructor_WithCorrectValues_AllPropertiesInitialized()
    {
        // Arrange
        var ceil = new Ceil();

        // Assert
        Assert.False(ceil.IsHited);
        Assert.False(ceil.ContainShip);
        Assert.Null(ceil.Ship);
        Assert.Null(ceil.ShipSegmentIndex);
    }

    [Fact]
    public void Constructor_WithShipSegment_SetsShipAndShipSegmentIndexCorrectly()
    {
        // Arrange
        var ceil = new Ceil();
        var ship = new Ship(Ship.Orientations.Vertical, 2);
        var shipSegmentIndex = 1;

        // Act
        ceil.WithShipSegment(ship, shipSegmentIndex);

        // Assert
        Assert.True(ceil.ContainShip);
        Assert.Equal(ship, ceil.Ship);
        Assert.Equal(shipSegmentIndex, ceil.ShipSegmentIndex);
    }

    [Fact]
    public void RemoveShipSegment_WithShipSegment_RemovesShipAndShipSegmentIndex()
    {
        // Arrange
        var ceil = new Ceil();
        var ship = new Ship(Ship.Orientations.Vertical, 2);
        var shipSegmentIndex = 1;
        ceil.WithShipSegment(ship, shipSegmentIndex);

        // Act
        ceil.RemoveShipSegment();

        // Assert
        Assert.False(ceil.ContainShip);
        Assert.Null(ceil.Ship);
        Assert.Null(ceil.ShipSegmentIndex);
    }

    [Fact]
    public void Hit_AlreadyHited_ReturnFalse()
    {
        // Arrange
        var ceil = new Ceil();
        var ship = new Ship(Ship.Orientations.Vertical, 2);
        var shipSegmentIndex = 1;
        ceil.WithShipSegment(ship, shipSegmentIndex);
        ceil.Hit();

        // Act
        var result = ceil.Hit();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Hit_OnNotHited_InvokeOnHited()
    {
        // Arrange
        var ceil = new Ceil();
        var ship = new Ship(Ship.Orientations.Vertical, 2);
        var shipSegmentIndex = 1;
        ceil.WithShipSegment(ship, shipSegmentIndex);
        bool onHitedInvoked = false;
        ceil.OnHited += () => onHitedInvoked = true;

        // Act
        var result = ceil.Hit();

        // Assert
        Assert.True(result);
        Assert.True(ceil.IsHited);
        Assert.True(onHitedInvoked);
    }
}
