namespace SeaBattle.Domain;

/// <summary>
/// Represents a ship in the game of Sea Battle.
/// </summary>
public class Ship : IDisposable
{
    /// <summary>
    /// Enumeration of possible ship orientations.
    /// </summary>
    public enum Orientations
    {
        Vertical,
        Horizontal
    }

    /// <summary>
    /// Event that is raised when the ship is hit.
    /// </summary>
    public event Action<bool, int>? OnHited;

    /// <summary>
    /// Indicates whether the ship is alive.
    /// </summary>
    public bool IsAlive { get; protected set; }

    /// <summary>Фф
    /// The orientation of the ship.
    /// </summary>
    public Orientations Orientation { get; protected set; }

    /// <summary>
    /// The length of the ship.
    /// </summary>
    public uint Length { get; protected set; }

    /// <summary>
    /// An array representing the segments of the ship.
    /// </summary>
    protected bool[] _aliveSegments;

    /// <summary>
    /// Initializes a new instance of the Ship class.
    /// </summary>
    /// <param name="orientation">The orientation of the ship.</param>
    /// <param name="length">The length of the ship.</param>
    public Ship(Orientations orientation, uint length)
    {
        Orientation = orientation;
        Length = length;
        _aliveSegments = new bool[Length];

        for (int i = _aliveSegments.Length - 1; i >= 0; i--)
            _aliveSegments[i] = true;

        IsAlive = true;
    }

    public void SetShipAlive()
    {

        for (int i = _aliveSegments.Length - 1; i >= 0; i--)
            _aliveSegments[i] = true;

        IsAlive = true;
    }

    /// <summary>
    /// Returns a value indicating whether the specified segment of the ship is alive.
    /// </summary>
    /// <param name="index">The index of the segment to check.</param>
    /// <returns>True if the specified segment is alive, otherwise false.</returns>
    public bool CheckSegment(int index) => _aliveSegments[index];

    /// <summary>
    /// Method that is called when the ship is hit.
    /// </summary>
    /// <param name="index">The index of the segment that was hit.</param>
    /// <returns>True if the hit was successful, otherwise false.</returns>
    public bool Hit(int index)
    {
        if (!_aliveSegments[index])
        {
            OnHited?.Invoke(false, index);
            return false;
        }

        _aliveSegments[index] = false;
        IsAlive = !_aliveSegments.All(x => x == false);
        OnHited?.Invoke(true, index);
        return true;
    }

    public void Dispose()
    {
        OnHited = null;
        _aliveSegments = new bool[0];
        IsAlive = false;
        Length = 0;
        Orientation = 0;
    }
}