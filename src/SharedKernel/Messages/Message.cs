using ShopDemo.SharedKernel.Messages.Enums;
using ShopDemo.SharedKernel.Messages.Exceptions;

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
        ValidateType(type);
        ValidateCode(code);

        Type = type;
        Code = code;
        Description = description;
    }

    // Builders
    public static Message Create(MessageType type, string code, string? description = null) => new(type, code, description);
    public static Message CreateSuccess(string code, string? description = null) => new(MessageType.Success, code, description);
    public static Message CreateWarning(string code, string? description = null) => new(MessageType.Warning, code, description);
    public static Message CreateError(string code, string? description = null) => new(MessageType.Error, code, description);
    public static Message CreateInformation(string code, string? description = null) => new(MessageType.Information, code, description);

    // Private Methods
    private static void ValidateType(MessageType type)
    {
        if (!Enum.IsDefined(type))
            throw new InvalidMessageTypeException(type);
    }
    private static void ValidateCode(string code)
    {
        if(string.IsNullOrWhiteSpace(code))
            throw new ArgumentNullException(nameof(code));
    }
}
