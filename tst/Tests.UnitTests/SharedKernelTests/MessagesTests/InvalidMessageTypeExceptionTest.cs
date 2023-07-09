using ShopDemo.SharedKernel.Messages.Enums;
using ShopDemo.SharedKernel.Messages.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDemo.Tests.UnitTests.SharedKernelTests.MessagesTests;
public class InvalidMessageTypeExceptionTest
{
    private const string CONTEXT = "SharedKernel";
    private const string OBJECT_NAME = nameof(InvalidMessageTypeException);

    [Fact(DisplayName = "Should correctly created")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void InvalidMessageTypeException_Should_Correctly_Created()
    {
        // Arrange
        var messageType = MessageType.Success;

        // Act
        var exception = new InvalidMessageTypeException(messageType);

        // Assert
        exception.MessageType.Should().Be(messageType);
    }
}
