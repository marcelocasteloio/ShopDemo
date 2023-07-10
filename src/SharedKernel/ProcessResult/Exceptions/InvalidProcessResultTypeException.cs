using ShopDemo.SharedKernel.ProcessResult.Enums;

namespace ShopDemo.SharedKernel.ProcessResult.Exceptions;

[Serializable]
public class InvalidProcessResultTypeException
    : Exception
{
    // Properties
    public ProcessResultType ProcessResultType { get; }

    // Constructors
    public InvalidProcessResultTypeException(ProcessResultType processResultType)
    {
        ProcessResultType = processResultType;
    }
}
