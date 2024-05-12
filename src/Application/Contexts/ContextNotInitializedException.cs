namespace SeaBattle.Application.Contexts;

/// <summary>
/// Represents an error that occurs when an attempt is made to access an application context that has not been initialized.
/// </summary>
[Serializable]
public class ContextNotInitializedException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ContextNotInitializedException"/> class.
    /// </summary>
    public ContextNotInitializedException() { }


    /// <summary>
    /// Initializes a new instance of the <see cref="ContextNotInitializedException"/> class with a specified context name.
    /// </summary>
    /// <param name="contextName">The name of the context that has not been initialized.</param>
    public ContextNotInitializedException(string contextName)
        : base($"Context '{contextName}' has not been initialized.") { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ContextNotInitializedException"/> class with a specified context name and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="contextName">The name of the context that has not been initialized.</param>
    /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public ContextNotInitializedException(string contextName, Exception inner)
        : base($"Context '{contextName}' has not been initialized.", inner) { }
}