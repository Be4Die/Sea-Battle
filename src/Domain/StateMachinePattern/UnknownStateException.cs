namespace SeaBattle.Domain.StateMachinePattern;

using System;

/// <summary>
/// The exception that is thrown when an attempt is made to access a state in a state machine that is not registered.
/// </summary>
[Serializable]
public class UnknownStateException : Exception
{
    public Type StateName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnknownStateException"/> class with a specified error message.
    /// </summary>
    /// <param name="stateType">The type of the state that caused the exception.</param>
    public UnknownStateException(Type stateType)
        : base($"State '{stateType}' is not registered.")
    {
        StateName = stateType;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnknownStateException"/> class with a specified error message and a reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <param name="stateType">The type of the state that caused the exception.</param>
    /// <param name="inner">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public UnknownStateException(Type stateType, Exception inner)
        : base($"State '{stateType.Name}' is not registered.", inner)
    {
        StateName = stateType;
    }
}