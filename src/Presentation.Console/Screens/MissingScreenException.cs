namespace SeaBattle.PresentationConsole.Screens;

// <summary>
/// The exception that is thrown when a screen is missing.
/// </summary>
[Serializable]
public class MissingScreenException : Exception
{
    /// <summary>
    /// The type of the screen that is missing.
    /// </summary>
    public Type ScreenType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MissingScreenException"/> class with a specified error message.
    /// </summary>
    /// <param name="screenType">The type of the screen that is missing.</param>
    public MissingScreenException(Type screenType)
        : base($"The screen of type '{screenType.Name}' is missing.")
    {
        ScreenType = screenType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MissingScreenException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="screenType">The type of the screen that is missing.</param>
    /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public MissingScreenException(Type screenType, Exception inner)
        : base($"The screen of type '{screenType.Name}' is missing.", inner)
    {
        ScreenType = screenType;
    }
}
