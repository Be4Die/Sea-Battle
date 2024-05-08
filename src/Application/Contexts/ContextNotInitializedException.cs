namespace SeaBattle.Application.Contexts;

/// <summary>
/// Represents an error that occurs when an attempt is made to access an application context that has not been initialized.
/// </summary>
[Serializable]
public class ContextNotInitializedException : Exception
{
    public ContextNotInitializedException() { }

    public ContextNotInitializedException(string contextName)
        : base($"Context '{contextName}' has not been initialized.") { }
    public ContextNotInitializedException(string contextName, Exception inner)
        : base($"Context '{contextName}' has not been initialized.", inner) { }
}