using ShopDemo.SharedKernel.Messages.Enums;

namespace ShopDemo.SharedKernel.Messages;
public readonly struct Message
{
    // Properties
    public MessageType Type { get; }
    public string Code { get; }
    public string? Description { get; }

    // Constructors
    private Message(
        MessageType type,
        string code,
        string? description
    )
    {
        Type = type;
        Code = code;
        Description = description;
    }

    // Builders
    public static Message Create(MessageType messageType, string code, string? description = null) => new(messageType, code, description);
    public static Message CreateSuccess(string code, string? description = null) => new(MessageType.Success, code, description);
    public static Message CreateWarning(string code, string? description = null) => new(MessageType.Warning, code, description);
    public static Message CreateError(string code, string? description = null) => new(MessageType.Error, code, description);
    public static Message CreateInformation(string code, string? description = null) => new(MessageType.Information, code, description);
}
