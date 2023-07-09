
using ShopDemo.SharedKernel.Messages.Enums;

namespace ShopDemo.SharedKernel.Messages.Exceptions;

[Serializable]
public class InvalidMessageTypeException
    : Exception
{
    // Properties
    public MessageType MessageType { get; }

    // Constructors
    public InvalidMessageTypeException(MessageType messageType)
    {
        MessageType = messageType;
    }
}
