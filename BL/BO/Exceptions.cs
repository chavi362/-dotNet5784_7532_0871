
namespace BO;


[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message,Exception ex) : base(message,ex) { }
}

[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message,Exception ex) : base(message,ex) { }
}
[Serializable]
public class LogicException : Exception
{
    public LogicException(string? message, Exception inner) : base(message,inner) { }
}
[Serializable]
public class BlDeletionImpossible : Exception
{
    public BlDeletionImpossible(string? message,Exception inner) : base(message, inner) { }
}
[Serializable]
public class BlNullPropertyException : Exception
{
    public BlNullPropertyException(string? message, Exception inner) : base(message, inner) { }
}
[Serializable]
public class BlInvalidPropertyException : Exception
{
    public BlInvalidPropertyException(string? message) : base(message) { }
}
