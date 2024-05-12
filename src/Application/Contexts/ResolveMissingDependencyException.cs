namespace SeaBattle.Application.Contexts;

/// <summary>
/// The exception that is thrown when an attempt is made to resolve a dependency that is not present in the dependency injection container.
/// </summary>
[Serializable]
public class ResolveMissingDependencyException : Exception
{
    public Type DependencyType { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResolveMissingDependencyException"/> class with a specified error message.
    /// </summary>
    /// <param name="dependencyType">The type of the dependency that caused the exception.</param>
    public ResolveMissingDependencyException(Type dependencyType)
        : base($"The dependency of type '{dependencyType.Name}' does not exist in the container.")
    {
        DependencyType = dependencyType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResolveMissingDependencyException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="dependencyType">The type of the dependency that caused the exception.</param>
    /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public ResolveMissingDependencyException(Type dependencyType, Exception inner)
        : base($"The dependency of type '{dependencyType.Name}' does not exist in the container.", inner)
    {
        DependencyType = dependencyType;
    }
}