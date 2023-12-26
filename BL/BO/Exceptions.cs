
namespace BO;


[Serializable]
public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}

[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
}
[Serializable]
public class LogicException : Exception
{
    public LogicException(string? message) : base(message) { }
}
[Serializable]
public class DalDeletionImpossible : Exception
{
    public DalDeletionImpossible(string? message) : base(message) { }
}
