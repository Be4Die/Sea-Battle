namespace Domain.UnitTests;


public class ShipTests
{
    [Fact]
    public void Constructor_WithCorrectValues_AliveAllSigments()
    {
        // Arrange
        var ship = new Ship(Ship.Orientations.Vertical, 3);

        // Act
        bool[] results = new bool[ship.Length];
        for (int i = 0; i < ship.Length; i++)
        {
            results[i] = ship.CheckSegment(i);
        }

        // Assert
        Assert.All(results, result => Assert.True(result));
    }

    [Fact]
    public void Constructor_WithCorrectValues_IsAlive()
    {
        // Arrange
        var ship = new Ship(Ship.Orientations.Vertical, 3);

        // Act
        var result = ship.IsAlive;

        // Assert
        Assert.True(result);
    }


    [Fact]
    public void Hit_SegmentIsAlive_SegmentIsMarkedAsDead()
    {
        // Arrange
        var ship = new Ship(Ship.Orientations.Vertical, 3);
        var index = 1;

        // Act
        ship.Hit(index);

        // Assert
        Assert.False(ship.CheckSegment(index));
    }

    [Fact]
    public void Hit_SegmentIsDead_SegmentIsNotChanged()
    {
        // Arrange
        var ship = new Ship(Ship.Orientations.Vertical, 3);
        var index = 1;
        ship.Hit(index);

        // Act
        ship.Hit(index);

        // Assert
        Assert.False(ship.CheckSegment(index));
    }

    [Fact]
    public void Hit_AllSegmentsAreDead_ShipIsDead()
    {
        // Arrange
        var ship = new Ship(Ship.Orientations.Vertical, 3);
        for (int i = (int)ship.Length - 1; i >= 0; i--)
        {
            ship.Hit(i);
        }

        // Act
        ship.Hit(0);

        // Assert
        Assert.False(ship.IsAlive);
    }

    [Fact]
    public void Hit_AllSegmentsAreAlive_ShipIsNotChanged()
    {
        // Arrange
        var ship = new Ship(Ship.Orientations.Vertical, 3);

        // Act
        ship.Hit(0);

        // Assert
        Assert.True(ship.IsAlive);
    }

    [Fact]
    public void CheckSegment_IndexOutOfRange_ThrowsIndexOutOfRange()
    {
        // Arrange
        var ship = new Ship(Ship.Orientations.Vertical, 3);
        var index = ship.Length + 1;

        // Act and Assert
        Assert.Throws<IndexOutOfRangeException>(() => ship.CheckSegment((int)index));
    }

    [Fact]
    public void CheckSegment_IndexWithinRange_ShouldReturnCorrectValue()
    {
        // Arrange
        var ship = new Ship(Ship.Orientations.Vertical, 3);
        var index = 0;

        // Act
        var result = ship.CheckSegment(index);

        // Assert
        Assert.True(result);
    }
}