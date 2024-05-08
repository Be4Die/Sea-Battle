namespace SeaBattle.Application.Contexts;


/// <summary>
/// The exception that is thrown when an attempt is made to inject a duplicate instance of a single type into a dependency injection container.
/// </summary>
[Serializable]
public class InjectDuplicateSingleInstanceException : Exception
{
    public Type InstanceType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="InjectDuplicateSingleInstanceException"/> class with a specified error message.
    /// </summary>
    /// <param name="instanceType">The type of the instance that caused the exception.</param>
    public InjectDuplicateSingleInstanceException(Type instanceType)
        : base($"An instance of type '{instanceType.Name}' has already been injected.")
    {
        InstanceType = instanceType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InjectDuplicateSingleInstanceException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="instanceType">The type of the instance that caused the exception.</param>
    /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public InjectDuplicateSingleInstanceException(Type instanceType, Exception inner)
        : base($"An instance of type '{instanceType.Name}' has already been injected.", inner)
    {
        InstanceType = instanceType;
    }
}
