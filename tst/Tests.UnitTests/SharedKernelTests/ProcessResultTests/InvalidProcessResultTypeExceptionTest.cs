using ShopDemo.SharedKernel.ProcessResult.Enums;
using ShopDemo.SharedKernel.ProcessResult.Exceptions;

namespace ShopDemo.Tests.UnitTests.SharedKernelTests.ProcessResultTests;
public class InvalidProcessResultTypeExceptionTest
{
    private const string CONTEXT = "SharedKernel";
    private const string OBJECT_NAME = nameof(InvalidProcessResultTypeException);

    [Fact(DisplayName = "Should correctly created")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void InvalidProcessResultTypeException_Should_Correctly_Created()
    {
        // Arrange
        var processResultType = ProcessResultType.Success;

        // Act
        var exception = new InvalidProcessResultTypeException(processResultType);

        // Assert
        exception.ProcessResultType.Should().Be(processResultType);
    }
}
