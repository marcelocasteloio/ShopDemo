using ShopDemo.SharedKernel.Messages;
using ShopDemo.SharedKernel.Messages.Enums;
using ShopDemo.SharedKernel.Messages.Exceptions;

namespace ShopDemo.Tests.UnitTests.SharedKernelTests.MessagesTests;
public class MessageTest
{
    private const string CONTEXT = "SharedKernel";
    private const string OBJECT_NAME = nameof(Message);

    [Fact(DisplayName = "Should correctly created")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void Message_Should_Correctly_Created()
    {
        // Arrange
        var messageTypeCollection = Enum.GetValues<MessageType>()!;
        var messageCollection = new Message[messageTypeCollection.Length];
        var code = "sample code";
        var description = "sample description";

        // Act
        for (int i = 0; i < messageTypeCollection.Length; i++)
            messageCollection[i] = Message.Create(type: messageTypeCollection[i], code, description);

        // Assert
        for (int i = 0; i < messageCollection.Length; i++)
        {
            var message = messageCollection[i];
            var messageType = messageTypeCollection[i];

            message.Type.Should().Be(messageType);
            message.Code.Should().Be(code);
            message.Description.Should().Be(description);
        }
    }

    [Fact(DisplayName = "Should correctly created without description")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void Message_Should_Correctly_Created_Without_Description()
    {
        // Arrange
        var messageTypeCollection = Enum.GetValues<MessageType>()!;
        var messageCollection = new Message[messageTypeCollection.Length];
        var code = "sample code";

        // Act
        for (int i = 0; i < messageTypeCollection.Length; i++)
            messageCollection[i] = Message.Create(type: messageTypeCollection[i], code, description: null);

        // Assert
        for (int i = 0; i < messageCollection.Length; i++)
        {
            var message = messageCollection[i];
            var messageType = messageTypeCollection[i];

            message.Type.Should().Be(messageType);
            message.Code.Should().Be(code);
            message.Description.Should().BeNull();
        }
    }

    [Fact(DisplayName = "Should correctly create success")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void Message_Should_Correctly_Create_Success()
    {
        // Arrange
        var expectedMessageType = MessageType.Success;
        var code = "sample code";
        var description = "sample description";

        // Act
        var message = Message.CreateSuccess(code, description);

        // Assert
        message.Type.Should().Be(expectedMessageType);
        message.Code.Should().Be(code);
        message.Description.Should().Be(description);
    }

    [Fact(DisplayName = "Should correctly create success without description")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void Message_Should_Correctly_Create_Success_Without_Description()
    {
        // Arrange
        var expectedMessageType = MessageType.Success;
        var code = "sample code";

        // Act
        var message = Message.CreateSuccess(code, description: null);

        // Assert
        message.Type.Should().Be(expectedMessageType);
        message.Code.Should().Be(code);
        message.Description.Should().BeNull();
    }

    [Fact(DisplayName = "Should correctly create information")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void Message_Should_Correctly_Create_Information()
    {
        // Arrange
        var expectedMessageType = MessageType.Information;
        var code = "sample code";
        var description = "sample description";

        // Act
        var message = Message.CreateInformation(code, description);

        // Assert
        message.Type.Should().Be(expectedMessageType);
        message.Code.Should().Be(code);
        message.Description.Should().Be(description);
    }

    [Fact(DisplayName = "Should correctly create information without description")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void Message_Should_Correctly_Create_Information_Without_Description()
    {
        // Arrange
        var expectedMessageType = MessageType.Information;
        var code = "sample code";

        // Act
        var message = Message.CreateInformation(code, description: null);

        // Assert
        message.Type.Should().Be(expectedMessageType);
        message.Code.Should().Be(code);
        message.Description.Should().BeNull();
    }

    [Fact(DisplayName = "Should correctly create warning")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void Message_Should_Correctly_Create_Warning()
    {
        // Arrange
        var expectedMessageType = MessageType.Warning;
        var code = "sample code";
        var description = "sample description";

        // Act
        var message = Message.CreateWarning(code, description);

        // Assert
        message.Type.Should().Be(expectedMessageType);
        message.Code.Should().Be(code);
        message.Description.Should().Be(description);
    }

    [Fact(DisplayName = "Should correctly create warning without description")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void Message_Should_Correctly_Create_Warning_Without_Description()
    {
        // Arrange
        var expectedMessageType = MessageType.Warning;
        var code = "sample code";

        // Act
        var message = Message.CreateWarning(code, description: null);

        // Assert
        message.Type.Should().Be(expectedMessageType);
        message.Code.Should().Be(code);
        message.Description.Should().BeNull();
    }

    [Fact(DisplayName = "Should correctly create error")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void Message_Should_Correctly_Create_Error()
    {
        // Arrange
        var expectedMessageType = MessageType.Error;
        var code = "sample code";
        var description = "sample description";

        // Act
        var message = Message.CreateError(code, description);

        // Assert
        message.Type.Should().Be(expectedMessageType);
        message.Code.Should().Be(code);
        message.Description.Should().Be(description);
    }

    [Fact(DisplayName = "Should correctly create error without description")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void Message_Should_Correctly_Create_Error_Without_Description()
    {
        // Arrange
        var expectedMessageType = MessageType.Error;
        var code = "sample code";

        // Act
        var message = Message.CreateError(code, description: null);

        // Assert
        message.Type.Should().Be(expectedMessageType);
        message.Code.Should().Be(code);
        message.Description.Should().BeNull();
    }

    [Fact(DisplayName = "Should not created with invalid type")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void Message_Should_Not_Created_With_Invalid_Type()
    {
        // Arrange
        var invalidMessageType = (MessageType)0;
        var code = "sample code";
        var hasThrownInvalidMessageTypeException = false;

        // Act
        try
        {
            _ = Message.Create(type: invalidMessageType, code: code);
        }
        catch (InvalidMessageTypeException ex)
        {
            hasThrownInvalidMessageTypeException = ex.MessageType == invalidMessageType;
        }

        // Assert
        hasThrownInvalidMessageTypeException.Should().BeTrue();
    }

    [Fact(DisplayName = "Should not created with invalid code")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void Message_Should_Not_Created_With_Invalid_Code()
    {
        // Arrange
        var MessageType = SharedKernel.Messages.Enums.MessageType.Success;
        var code = string.Empty;
        var hasThrownArgumentNullException = false;
        
        // Act
        try
        {
            _ = Message.Create(type: MessageType, code: code);
        }
        catch (ArgumentNullException ex)
        {
            hasThrownArgumentNullException = ex.ParamName == "code";
        }

        // Assert
        hasThrownArgumentNullException.Should().BeTrue();
    }
}
