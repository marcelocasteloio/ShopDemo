using ShopDemo.SharedKernel.Messages;
using ShopDemo.SharedKernel.Messages.Enums;
using ShopDemo.SharedKernel.ProcessResult.Enums;

namespace ShopDemo.SharedKernel.ProcessResult;
public readonly struct ProcessResult
{
    // Properties
    public ProcessResultType Type { get; }
    public bool IsSuccess => Type == ProcessResultType.Success;
    public bool IsError => Type == ProcessResultType.Error;
    public bool IsPartial => Type == ProcessResultType.Partial;
    public IEnumerable<Message> MessageCollection { get; }

    // Constructors
    private ProcessResult(
        ProcessResultType type, 
        IEnumerable<Message>? messageCollection
    )
    {
        Type = type;
        MessageCollection = messageCollection ?? Enumerable.Empty<Message>();
    }

    // Operators
    public static implicit operator ProcessResult(bool value) => value ? CreateSuccess() : CreateError();
    public static implicit operator bool(ProcessResult value) => value.IsSuccess;

    // Builders
    public static ProcessResult Create(ProcessResultType type, IEnumerable<Message>? messageCollection = null) => new(type, messageCollection);
    public static ProcessResult CreateSuccess(IEnumerable<Message>? messageCollection = null) => new(ProcessResultType.Success, messageCollection);
    public static ProcessResult CreateError(IEnumerable<Message>? messageCollection = null) => new(ProcessResultType.Error, messageCollection);
    public static ProcessResult CreatePartial(IEnumerable<Message>? messageCollection = null) => new(ProcessResultType.Partial, messageCollection);
    public static ProcessResult CreateBasedOnMessageCollection(IEnumerable<Message>? messageCollection = null)
    {
        if (messageCollection is null || !messageCollection.Any())
            return CreateSuccess();

        if (messageCollection.Any(message => message.Type == MessageType.Error))
            return messageCollection.Any(message => message.Type == MessageType.Success)
                ? CreatePartial(messageCollection)
                : CreateError(messageCollection);
        else
            return CreateSuccess(messageCollection);
    }
}
public readonly struct ProcessResult<TOutput>
{
    // Properties
    public ProcessResultType Type { get; }
    public bool IsSuccess => Type == ProcessResultType.Success;
    public bool IsError => Type == ProcessResultType.Error;
    public bool IsPartial => Type == ProcessResultType.Partial;
    public IEnumerable<Message> MessageCollection { get; }
    public TOutput? Output { get; }
    public bool HasOutput => Output is not null;

    // Constructors
    private ProcessResult(
        ProcessResultType type,
        IEnumerable<Message>? messageCollection,
        TOutput? output
    )
    {
        Type = type;
        MessageCollection = messageCollection ?? Enumerable.Empty<Message>();
        Output = output;
    }

    // Operators
    public static implicit operator ProcessResult<TOutput>(bool value) => value ? CreateSuccess(output: default) : CreateError(output: default);
    public static implicit operator bool(ProcessResult<TOutput> value) => value.IsSuccess;

    // Builders
    public static ProcessResult<TOutput> Create(ProcessResultType type, TOutput? output, IEnumerable<Message>? messageCollection = null) => new(type, messageCollection, output);
    public static ProcessResult<TOutput> CreateSuccess(TOutput? output, IEnumerable<Message>? messageCollection = null) => new(ProcessResultType.Success, messageCollection, output);
    public static ProcessResult<TOutput> CreateError(TOutput? output, IEnumerable<Message>? messageCollection = null) => new(ProcessResultType.Error, messageCollection, output);
    public static ProcessResult<TOutput> CreatePartial(TOutput? output, IEnumerable<Message>? messageCollection = null) => new(ProcessResultType.Partial, messageCollection, output);
    public static ProcessResult<TOutput> CreateBasedOnMessageCollection(TOutput? output, IEnumerable<Message>? messageCollection = null)
    {
        if (messageCollection is null || !messageCollection.Any())
            return CreateSuccess(output);

        if (messageCollection.Any(message => message.Type == MessageType.Error))
            return messageCollection.Any(message => message.Type == MessageType.Success)
                ? CreatePartial(output, messageCollection)
                : CreateError(output, messageCollection);
        else
            return CreateSuccess(output, messageCollection);
    }
}
