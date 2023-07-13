using ShopDemo.SharedKernel.Messages;
using ShopDemo.SharedKernel.Messages.Enums;
using ShopDemo.SharedKernel.ProcessResult.Enums;
using ShopDemo.SharedKernel.ProcessResult.Exceptions;

namespace ShopDemo.SharedKernel.ProcessResult;
public readonly struct ProcessResult
{
    // Properties
    public ProcessResultType Type { get; }
    public bool IsSuccess => Type == ProcessResultType.Success;
    public bool IsError => Type == ProcessResultType.Error;
    public bool IsPartial => Type == ProcessResultType.Partial;
    public Message[]? MessageCollection { get; }

    // Constructors
    private ProcessResult(
        ProcessResultType type, 
        Message[]? messageCollection
    )
    {
        ValidateType(type);

        Type = type;
        MessageCollection = messageCollection ?? Array.Empty<Message>();
    }

    // Operators
    public static implicit operator ProcessResult(bool value) => value ? CreateSuccess() : CreateError();
    public static implicit operator bool(ProcessResult value) => value.IsSuccess;

    // Builders
    public static ProcessResult Create(ProcessResultType type, Message[]? messageCollection = null) => new(type, messageCollection);
    public static ProcessResult CreateSuccess(Message[]? messageCollection = null) => new(ProcessResultType.Success, messageCollection);
    public static ProcessResult CreateError(Message[]? messageCollection = null) => new(ProcessResultType.Error, messageCollection);
    public static ProcessResult CreatePartial(Message[]? messageCollection = null) => new(ProcessResultType.Partial, messageCollection);
    public static ProcessResult FromProcessResultWithOutput<TOutput>(ProcessResult<TOutput> processResult) => Create(processResult.Type, processResult.MessageCollection);
    public static ProcessResult FromMessageCollection(Message[]? messageCollection = null)
    {
        if (messageCollection is null || messageCollection.Length == 0)
            return CreateSuccess();

        if (Array.Exists(messageCollection, message => message.Type == MessageType.Error))
            return Array.Exists(messageCollection, message => message.Type == MessageType.Success)
                ? CreatePartial(messageCollection)
                : CreateError(messageCollection);
        else
            return CreateSuccess(messageCollection);
    }
    public static ProcessResult FromProcessResult(ProcessResult processResult)
        => new(processResult.Type, processResult.MessageCollection);
    public static ProcessResult FromProcessResult(params ProcessResult[] processResultCollection)
    {
        // Count total messages
        var totalMessages = 0;
        for (int i = 0; i < processResultCollection.Length; i++)
            totalMessages += processResultCollection[i].MessageCollection?.Length ?? 0;

        ProcessResultType? newProcessResultType = null;
        var newMessageArray = new Message[totalMessages];
        var lastIndex = 0;

        for (int i = 0; i < processResultCollection.Length; i++)
        {
            var processResult = processResultCollection[i];

            // Analyse process type
            if (newProcessResultType is null)
                newProcessResultType = processResult.Type;
            /*
             * If the type is already an error or partial, 
             * no matter the other statuses, 
             * the result will always be same.
             * We will analyse another status only
             * if status is success.
             * 
             * If processResult.Type is success, the
             * newProcessResultType cannot change, because
             * this, we will change the newProcessResult
             * status only if processResult.Type is differente
             * of success
             */
            else if (newProcessResultType == ProcessResultType.Success 
                && processResult.Type != ProcessResultType.Success)
            {
                newProcessResultType = processResult.Type;
            }

            // Copy messages
            if (processResult.MessageCollection is null)
                continue;

            Array.Copy(
                sourceArray: processResult.MessageCollection,
                sourceIndex: 0,
                destinationArray: newMessageArray,
                destinationIndex: lastIndex,
                length: processResult.MessageCollection.Length
            );

            lastIndex += processResult.MessageCollection.Length;
        }

        return new ProcessResult(newProcessResultType ?? ProcessResultType.Success, newMessageArray);
    }

    // Private Methods
    private static void ValidateType(ProcessResultType type)
    {
        if (!Enum.IsDefined(type))
            throw new InvalidProcessResultTypeException(processResultType: type);
    }
}
public readonly struct ProcessResult<TOutput>
{
    // Properties
    public ProcessResultType Type { get; }
    public bool IsSuccess => Type == ProcessResultType.Success;
    public bool IsError => Type == ProcessResultType.Error;
    public bool IsPartial => Type == ProcessResultType.Partial;
    public Message[]? MessageCollection { get; }
    public TOutput? Output { get; }
    public bool HasOutput => Output is not null;

    // Constructors
    private ProcessResult(
        ProcessResultType type,
        Message[]? messageCollection,
        TOutput? output
    )
    {
        ValidateType(type);

        Type = type;
        MessageCollection = messageCollection ?? Array.Empty<Message>();
        Output = output;
    }

    // Operators
    public static implicit operator ProcessResult<TOutput>(bool value) => value ? CreateSuccess(output: default) : CreateError(output: default);
    public static implicit operator bool(ProcessResult<TOutput> value) => value.IsSuccess;
    public static implicit operator ProcessResult<TOutput>(ProcessResult value) => Create(value.Type, output: default, value.MessageCollection);

    // Builders
    public static ProcessResult<TOutput> Create(ProcessResultType type, TOutput? output, Message[]? messageCollection = null) => new(type, messageCollection, output);
    public static ProcessResult<TOutput> CreateSuccess(TOutput? output, Message[]? messageCollection = null) => new(ProcessResultType.Success, messageCollection, output);
    public static ProcessResult<TOutput> CreateError(TOutput? output, Message[]? messageCollection = null) => new(ProcessResultType.Error, messageCollection, output);
    public static ProcessResult<TOutput> CreatePartial(TOutput? output, Message[]? messageCollection = null) => new(ProcessResultType.Partial, messageCollection, output);
    public static ProcessResult<TOutput> FromMessageCollection(TOutput? output, Message[]? messageCollection = null)
    {
        if (messageCollection is null || messageCollection.Length == 0)
            return CreateSuccess(output);

        if (Array.Exists(messageCollection, message => message.Type == MessageType.Error))
            return Array.Exists(messageCollection, message => message.Type == MessageType.Success)
                ? CreatePartial(output, messageCollection)
                : CreateError(output, messageCollection);
        else
            return CreateSuccess(output, messageCollection);
    }
    public static ProcessResult<TOutput> FromProcessResult(TOutput? output, ProcessResult<TOutput> processResult)
        => new(processResult.Type, processResult.MessageCollection, output);
    public static ProcessResult<TOutput> FromProcessResult(TOutput output, params ProcessResult<TOutput>[] processResultCollection)
    {
        // Count total messages
        var totalMessages = 0;
        for (int i = 0; i < processResultCollection.Length; i++)
            totalMessages += processResultCollection[i].MessageCollection?.Length ?? 0;

        ProcessResultType? newProcessResultType = null;
        var newMessageArray = new Message[totalMessages];
        var lastIndex = 0;

        for (int i = 0; i < processResultCollection.Length; i++)
        {
            var processResult = processResultCollection[i];

            // Analyse process type
            if (newProcessResultType is null)
                newProcessResultType = processResult.Type;
            /*
             * If the type is already an error or partial, 
             * no matter the other statuses, 
             * the result will always be same.
             * We will analyse another status only
             * if status is success.
             * 
             * If processResult.Type is success, the
             * newProcessResultType cannot change, because
             * this, we will change the newProcessResult
             * status only if processResult.Type is differente
             * of success
             */
            else if (newProcessResultType == ProcessResultType.Success
                && processResult.Type != ProcessResultType.Success)
            {
                newProcessResultType = processResult.Type;
            }

            // Copy messages
            if (processResult.MessageCollection is null)
                continue;

            Array.Copy(
                sourceArray: processResult.MessageCollection,
                sourceIndex: 0,
                destinationArray: newMessageArray,
                destinationIndex: lastIndex,
                length: processResult.MessageCollection.Length
            );

            lastIndex += processResult.MessageCollection.Length;
        }

        return new ProcessResult<TOutput>(newProcessResultType ?? ProcessResultType.Success, newMessageArray, output);
    }
    // Private Methods
    private static void ValidateType(ProcessResultType type)
    {
        if (!Enum.IsDefined(type))
            throw new InvalidProcessResultTypeException(processResultType: type);
    }
}
