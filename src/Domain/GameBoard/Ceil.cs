namespace SeaBattle.Domain.GameBoard;

/// <summary>
/// Represents a single cell on the <seealso cref="Board"/>.
/// </summary>
public class Ceil : IDisposable
{
    /// <summary>
    /// Event that is raised when the cell is hit.
    /// </summary>
    public event Action? OnHited;

    /// <summary>
    /// Indicates whether the cell has been hit.
    /// </summary>
    public bool IsHited { get; private set; }

    /// <summary>
    /// Indicates the ceil contain ship part.
    /// </summary>
    public bool ContainShip  => Ship != null && ShipSegmentIndex != null;

    /// <summary>
    /// The ship that occupies the cell. Null if the cell is empty.
    /// </summary>
    public Ship? Ship { get; private set; }

    /// <summary>
    /// The index of the segment of the ship that occupies the cell.
    /// </summary>
    public int? ShipSegmentIndex { get; private set; }

    /// <summary>
    /// Initializes a new instance of the Ceil class.
    /// </summary>
    public Ceil()
    {
        IsHited = false;
        Ship = null;
        ShipSegmentIndex = null;
    }

    /// <summary>
    /// Fluent constructor make ceil with ship.
    /// </summary>
    /// <param name="ship">The ship to place.</param>
    /// <param name="shipSegmentIndex">The index of the segment of the ship.</param>
    /// <returns>The current instance of the Ceil class.</returns>
    public Ceil WithShipSegment(Ship ship, int shipSegmentIndex)
    {
        PlaceShipSegment(ship, shipSegmentIndex);
        return this;
    }

    /// <summary>
    /// Removes the ship segment from the cell.
    /// </summary>
    public void RemoveShipSegment()
    {
        Ship = null;
        ShipSegmentIndex = null;
    }

    /// <summary>
    /// Places a ship on the cell.
    /// </summary>
    /// <param name="ship">The ship to place.</param>
    /// <param name="shipSegmentIndex">The index of the segment of the ship.</param>
    /// <returns>The current instance of the Ceil class.</returns>
    public void PlaceShipSegment(Ship ship, int shipSegmentIndex)
    {
        Ship = ship;
        ShipSegmentIndex = shipSegmentIndex;
    }

    /// <summary>
    /// Method that is called when the cell is hit.
    /// </summary>
    /// <returns>True if the hit was successful, otherwise false.</returns>
    public bool Hit()
    {
        // If the cell has already been hit, or if it does not contain a ship, or if the ship segment index is not set,
        // the hit is not successful.
        if (IsHited)
            return false;

        IsHited = true;

        if (Ship == null || ShipSegmentIndex == null)
            return false;
        // Inform the ship that it has been hit.
        Ship.Hit(ShipSegmentIndex.Value);
        // Raise the OnHit event.
        OnHited?.Invoke();
        return true;
    }

    /// <summary>
    /// Releases unmanaged resources used by the object.
    /// </summary>
    /// <remarks>
    /// This method sets the reference to the ship and the ship segment index to null, and also sets the event delegate `OnHited` to null to prevent memory leaks.
    /// </remarks>
    public void Dispose()
    {
        Ship = null;
        ShipSegmentIndex = null;
        OnHited = null;
        GC.SuppressFinalize(this);
    }
}