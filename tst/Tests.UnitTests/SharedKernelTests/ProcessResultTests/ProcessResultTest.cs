using ShopDemo.SharedKernel.Messages;
using ShopDemo.SharedKernel.Messages.Enums;
using ShopDemo.SharedKernel.ProcessResult;
using ShopDemo.SharedKernel.ProcessResult.Enums;
using ShopDemo.SharedKernel.ProcessResult.Exceptions;

namespace ShopDemo.Tests.UnitTests.SharedKernelTests.ProcessResultTests;
public class ProcessResultTest
{
    private const string CONTEXT = "SharedKernel";
    private const string OBJECT_NAME = nameof(ProcessResult);

    [Fact(DisplayName = "Should create without message")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Create_Without_Message()
    {
        // Arrange
        var processResultType = ProcessResultType.Success;

        // Act
        var processResultCollection = new[]
        {
            ProcessResult.Create(processResultType),
            ProcessResult.CreateSuccess(),
            ProcessResult.FromMessageCollection()
        };

        // Assert
        var validateAction = new Action<ProcessResult>(processResult =>
        {
            processResult.Type.Should().Be(processResultType);
            processResult.IsSuccess.Should().BeTrue();
            processResult.IsPartial.Should().BeFalse();
            processResult.IsError.Should().BeFalse();

            processResult.MessageCollection.Should().BeEmpty();
        });

        foreach (var processResult in processResultCollection)
            validateAction(processResult);
    }

    [Fact(DisplayName = "Should create without message using generic")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Create_Without_Message_Using_Generic()
    {
        // Arrange
        var processResultType = ProcessResultType.Success;

        // Act
        var processResultCollection = new[]
        {
            ProcessResult<object>.Create(processResultType, output: null),
            ProcessResult<object>.CreateSuccess(output: null),
            ProcessResult<object>.FromMessageCollection(output: null)
        };

        // Assert
        var validateAction = new Action<ProcessResult<object>>(processResult =>
        {
            processResult.Type.Should().Be(processResultType);
            processResult.IsSuccess.Should().BeTrue();
            processResult.IsPartial.Should().BeFalse();
            processResult.IsError.Should().BeFalse();
            processResult.HasOutput.Should().BeFalse();

            processResult.MessageCollection.Should().BeEmpty();
        });

        foreach (var processResult in processResultCollection)
            validateAction(processResult);
    }

    [Fact(DisplayName = "Should create with output and without message")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Create_With_Output_And_Without_Message()
    {
        // Arrange
        var processResultType = ProcessResultType.Success;
        var output = new { Id = 1, Name = "sample object" };

        // Act
        var processResultCollection = new[]
        {
            ProcessResult<object>.Create(processResultType, output),
            ProcessResult<object>.CreateSuccess(output),
            ProcessResult<object>.FromMessageCollection(output)
        };

        // Assert
        var validateAction = new Action<ProcessResult<object>>(processResult =>
        {
            processResult.Type.Should().Be(processResultType);
            processResult.IsSuccess.Should().BeTrue();
            processResult.IsPartial.Should().BeFalse();
            processResult.IsError.Should().BeFalse();

            processResult.Output.Should().BeSameAs(output);
            processResult.HasOutput.Should().BeTrue();

            processResult.MessageCollection.Should().BeEmpty();
        });

        foreach (var processResult in processResultCollection)
            validateAction(processResult);
    }

    [Fact(DisplayName = "Should create success")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Create_Success()
    {
        // Arrange
        var processResultType = ProcessResultType.Success;
        var successMessageCode = "success";
        var informationMessageCode = "information";
        var warningMessageCode = "warning";
        var errorMessageCode = "error";

        var messageCollection = new[] {
            Message.CreateSuccess(code: successMessageCode),
            Message.CreateInformation(code: informationMessageCode),
            Message.CreateWarning(code: warningMessageCode),
            Message.CreateError(code: errorMessageCode)
        };

        // Act
        var processResultCollection = new[]
        {
            ProcessResult.Create(processResultType, messageCollection),
            ProcessResult.CreateSuccess(messageCollection)
        };

        // Assert
        var validateAction = new Action<ProcessResult>(processResult =>
        {
            processResult.Type.Should().Be(processResultType);
            processResult.IsSuccess.Should().BeTrue();
            processResult.IsPartial.Should().BeFalse();
            processResult.IsError.Should().BeFalse();

            processResult.MessageCollection.Should().HaveCount(4);
            processResult.MessageCollection.Any(q => q.Code.Equals(successMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(informationMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(warningMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(errorMessageCode)).Should().BeTrue();
        });

        foreach (var processResult in processResultCollection)
            validateAction(processResult);
    }

    [Fact(DisplayName = "Should create success with output")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Create_Success_With_Output()
    {
        // Arrange
        var processResultType = ProcessResultType.Success;
        var successMessageCode = "success";
        var informationMessageCode = "information";
        var warningMessageCode = "warning";
        var errorMessageCode = "error";
        var output = new { Id = 1, Name = "sample object" };

        var messageCollection = new[] {
            Message.CreateSuccess(code: successMessageCode),
            Message.CreateInformation(code: informationMessageCode),
            Message.CreateWarning(code: warningMessageCode),
            Message.CreateError(code: errorMessageCode)
        };

        // Act
        var processResultCollection = new[]
        {
            ProcessResult<object>.Create(processResultType, output, messageCollection),
            ProcessResult<object>.CreateSuccess(output, messageCollection)
        };

        // Assert
        var validateAction = new Action<ProcessResult<object>>(processResult =>
        {
            processResult.Type.Should().Be(processResultType);
            processResult.IsSuccess.Should().BeTrue();
            processResult.IsPartial.Should().BeFalse();
            processResult.IsError.Should().BeFalse();

            processResult.Output.Should().BeSameAs(output);
            processResult.HasOutput.Should().BeTrue();

            processResult.MessageCollection.Should().HaveCount(4);
            processResult.MessageCollection.Any(q => q.Code.Equals(successMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(informationMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(warningMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(errorMessageCode)).Should().BeTrue();
        });

        foreach (var processResult in processResultCollection)
            validateAction(processResult);
    }

    [Fact(DisplayName = "Should create error")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Create_Error()
    {
        // Arrange
        var processResultType = ProcessResultType.Error;
        var successMessageCode = "success";
        var informationMessageCode = "information";
        var warningMessageCode = "warning";
        var errorMessageCode = "error";

        var messageCollection = new[] {
            Message.CreateSuccess(code: successMessageCode),
            Message.CreateInformation(code: informationMessageCode),
            Message.CreateWarning(code: warningMessageCode),
            Message.CreateError(code: errorMessageCode)
        };

        // Act
        var processResultCollection = new[]
        {
            ProcessResult.Create(processResultType, messageCollection),
            ProcessResult.CreateError(messageCollection)
        };

        // Assert
        var validateAction = new Action<ProcessResult>(processResult =>
        {
            processResult.Type.Should().Be(processResultType);
            processResult.IsSuccess.Should().BeFalse();
            processResult.IsPartial.Should().BeFalse();
            processResult.IsError.Should().BeTrue();

            processResult.MessageCollection.Should().HaveCount(4);
            processResult.MessageCollection.Any(q => q.Code.Equals(successMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(informationMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(warningMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(errorMessageCode)).Should().BeTrue();
        });

        foreach (var processResult in processResultCollection)
            validateAction(processResult);
    }

    [Fact(DisplayName = "Should create error with output")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Create_Error_With_Output()
    {
        // Arrange
        var processResultType = ProcessResultType.Error;
        var successMessageCode = "success";
        var informationMessageCode = "information";
        var warningMessageCode = "warning";
        var errorMessageCode = "error";
        var output = new { Id = 1, Name = "sample object" };

        var messageCollection = new[] {
            Message.CreateSuccess(code: successMessageCode),
            Message.CreateInformation(code: informationMessageCode),
            Message.CreateWarning(code: warningMessageCode),
            Message.CreateError(code: errorMessageCode)
        };

        // Act
        var processResultCollection = new[]
        {
            ProcessResult<object>.Create(processResultType, output, messageCollection),
            ProcessResult<object>.CreateError(output, messageCollection)
        };

        // Assert
        var validateAction = new Action<ProcessResult<object>>(processResult =>
        {
            processResult.Type.Should().Be(processResultType);
            processResult.IsSuccess.Should().BeFalse();
            processResult.IsPartial.Should().BeFalse();
            processResult.IsError.Should().BeTrue();

            processResult.Output.Should().BeSameAs(output);
            processResult.HasOutput.Should().BeTrue();

            processResult.MessageCollection.Should().HaveCount(4);
            processResult.MessageCollection.Any(q => q.Code.Equals(successMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(informationMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(warningMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(errorMessageCode)).Should().BeTrue();
        });

        foreach (var processResult in processResultCollection)
            validateAction(processResult);
    }

    [Fact(DisplayName = "Should create partial")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Create_Partial()
    {
        // Arrange
        var processResultType = ProcessResultType.Partial;
        var successMessageCode = "success";
        var informationMessageCode = "information";
        var warningMessageCode = "warning";
        var errorMessageCode = "error";

        var messageCollection = new[] {
            Message.CreateSuccess(code: successMessageCode),
            Message.CreateInformation(code: informationMessageCode),
            Message.CreateWarning(code: warningMessageCode),
            Message.CreateError(code: errorMessageCode)
        };

        // Act
        var processResultCollection = new[]
        {
            ProcessResult.Create(processResultType, messageCollection),
            ProcessResult.CreatePartial(messageCollection)
        };

        // Assert
        var validateAction = new Action<ProcessResult>(processResult =>
        {
            processResult.Type.Should().Be(processResultType);
            processResult.IsSuccess.Should().BeFalse();
            processResult.IsPartial.Should().BeTrue();
            processResult.IsError.Should().BeFalse();

            processResult.MessageCollection.Should().HaveCount(4);
            processResult.MessageCollection.Any(q => q.Code.Equals(successMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(informationMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(warningMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(errorMessageCode)).Should().BeTrue();
        });

        foreach (var processResult in processResultCollection)
            validateAction(processResult);
    }

    [Fact(DisplayName = "Should create partial with output")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Create_Partial_With_Output()
    {
        // Arrange
        var processResultType = ProcessResultType.Partial;
        var successMessageCode = "success";
        var informationMessageCode = "information";
        var warningMessageCode = "warning";
        var errorMessageCode = "error";
        var output = new { Id = 1, Name = "sample object" };

        var messageCollection = new[] {
            Message.CreateSuccess(code: successMessageCode),
            Message.CreateInformation(code: informationMessageCode),
            Message.CreateWarning(code: warningMessageCode),
            Message.CreateError(code: errorMessageCode)
        };

        // Act
        var processResultCollection = new[]
        {
            ProcessResult<object>.Create(processResultType, output, messageCollection),
            ProcessResult<object>.CreatePartial(output, messageCollection)
        };

        // Assert
        var validateAction = new Action<ProcessResult<object>>(processResult =>
        {
            processResult.Type.Should().Be(processResultType);
            processResult.IsSuccess.Should().BeFalse();
            processResult.IsPartial.Should().BeTrue();
            processResult.IsError.Should().BeFalse();

            processResult.Output.Should().BeSameAs(output);
            processResult.HasOutput.Should().BeTrue();

            processResult.MessageCollection.Should().HaveCount(4);
            processResult.MessageCollection.Any(q => q.Code.Equals(successMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(informationMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(warningMessageCode)).Should().BeTrue();
            processResult.MessageCollection.Any(q => q.Code.Equals(errorMessageCode)).Should().BeTrue();
        });

        foreach (var processResult in processResultCollection)
            validateAction(processResult);
    }

    [Fact(DisplayName = "Should create from process result with output")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Create_From_Process_Result_With_Output()
    {
        // Arrange
        var processResultType = ProcessResultType.Partial;
        var successMessageCode = "success";
        var informationMessageCode = "information";
        var warningMessageCode = "warning";
        var errorMessageCode = "error";
        var output = new { Id = 1, Name = "sample object" };

        var messageCollection = new[] {
            Message.CreateSuccess(code: successMessageCode),
            Message.CreateInformation(code: informationMessageCode),
            Message.CreateWarning(code: warningMessageCode),
            Message.CreateError(code: errorMessageCode)
        };

        var processResultWithOutput = ProcessResult<object>.Create(processResultType, output, messageCollection);

        // Act
        var processResult = ProcessResult.FromProcessResultWithOutput(processResultWithOutput);

        // Arrange
        processResult.Type.Should().Be(processResultWithOutput.Type);
        processResult.IsSuccess.Should().Be(processResultWithOutput.IsSuccess);
        processResult.IsPartial.Should().Be(processResultWithOutput.IsPartial);
        processResult.IsError.Should().Be(processResultWithOutput.IsError);

        processResult.MessageCollection.Should().BeSameAs(processResultWithOutput.MessageCollection);
    }

    [Fact(DisplayName = "Should create success from message collection")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Create_Success_From_Message_Collection()
    {
        // Arrange
        var successMessageCode = "success";
        var informationMessageCode = "information";
        var warningMessageCode = "warning";
        var errorMessageCode = "error";
        var output = new { Id = 1, Name = "sample object" };

        var messageCollection = new[] {
            Message.CreateSuccess(code: successMessageCode),
            Message.CreateInformation(code: informationMessageCode),
            Message.CreateWarning(code: warningMessageCode),
            Message.CreateError(code: errorMessageCode)
        }.Where(q => q.Type == MessageType.Information || q.Type == MessageType.Success)!;

        // Act
        var processResultCollection = new[]
        {
            ProcessResult.FromMessageCollection(messageCollection),
            ProcessResult<object>.FromMessageCollection(output, messageCollection)
        };

        // Arrange
        var validateAction = new Action<ProcessResult<object>>(processResult =>
        {
            processResult.Type.Should().Be(ProcessResultType.Success);
            processResult.IsSuccess.Should().BeTrue();
            processResult.IsPartial.Should().BeFalse();
            processResult.IsError.Should().BeFalse();

            processResult.MessageCollection.Should().BeSameAs(messageCollection);
        });

        foreach (var processResult in processResultCollection)
            validateAction(processResult);
    }

    [Fact(DisplayName = "Should create partial from message collection")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Create_Partial_From_Message_Collection()
    {
        // Arrange
        var successMessageCode = "success";
        var informationMessageCode = "information";
        var warningMessageCode = "warning";
        var errorMessageCode = "error";
        var output = new { Id = 1, Name = "sample object" };

        var messageCollection = new[] {
            Message.CreateSuccess(code: successMessageCode),
            Message.CreateInformation(code: informationMessageCode),
            Message.CreateWarning(code: warningMessageCode),
            Message.CreateError(code: errorMessageCode)
        };

        // Act
        var processResultCollection = new[]
        {
            ProcessResult.FromMessageCollection(messageCollection),
            ProcessResult<object>.FromMessageCollection(output, messageCollection)
        };

        // Arrange
        var validateAction = new Action<ProcessResult<object>>(processResult =>
        {
            processResult.Type.Should().Be(ProcessResultType.Partial);
            processResult.IsSuccess.Should().BeFalse();
            processResult.IsPartial.Should().BeTrue();
            processResult.IsError.Should().BeFalse();

            processResult.MessageCollection.Should().BeSameAs(messageCollection);
        });

        foreach (var processResult in processResultCollection)
            validateAction(processResult);
    }

    [Fact(DisplayName = "Should create error from message collection")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Create_Error_From_Message_Collection()
    {
        // Arrange
        var successMessageCode = "success";
        var informationMessageCode = "information";
        var warningMessageCode = "warning";
        var errorMessageCode = "error";
        var output = new { Id = 1, Name = "sample object" };

        var messageCollection = new[] {
            Message.CreateSuccess(code: successMessageCode),
            Message.CreateInformation(code: informationMessageCode),
            Message.CreateWarning(code: warningMessageCode),
            Message.CreateError(code: errorMessageCode)
        }.Where(q => q.Type != MessageType.Success);

        // Act
        var processResultCollection = new[]
        {
            ProcessResult.FromMessageCollection(messageCollection),
            ProcessResult<object>.FromMessageCollection(output, messageCollection)
        };

        // Arrange
        var validateAction = new Action<ProcessResult<object>>(processResult =>
        {
            processResult.Type.Should().Be(ProcessResultType.Error);
            processResult.IsSuccess.Should().BeFalse();
            processResult.IsPartial.Should().BeFalse();
            processResult.IsError.Should().BeTrue();

            processResult.MessageCollection.Should().BeSameAs(messageCollection);
        });

        foreach (var processResult in processResultCollection)
            validateAction(processResult);
    }

    [Fact(DisplayName = "Should not created with invalid ProcessResultType")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Not_Created_With_Invalid_ProcessResultType()
    {
        // Arrange
        var invalidProcessResultType = (ProcessResultType)0;
        var successMessageCode = "success";
        var informationMessageCode = "information";
        var warningMessageCode = "warning";
        var errorMessageCode = "error";
        var output = new { Id = 1, Name = "sample object" };

        var messageCollection = new[] {
            Message.CreateSuccess(code: successMessageCode),
            Message.CreateInformation(code: informationMessageCode),
            Message.CreateWarning(code: warningMessageCode),
            Message.CreateError(code: errorMessageCode)
        }.Where(q => q.Type != MessageType.Success);

        var hasThrownExceptionCollection = new bool[2];

        // Act
        try
        {
            _ = ProcessResult.Create(invalidProcessResultType, messageCollection);
        }
        catch (InvalidProcessResultTypeException ex)
        {
            hasThrownExceptionCollection[0] = ex.ProcessResultType == invalidProcessResultType;
        }
        try
        {
            _ = ProcessResult<object>.Create(invalidProcessResultType, output, messageCollection);
        }
        catch (InvalidProcessResultTypeException ex)
        {
            hasThrownExceptionCollection[1] = ex.ProcessResultType == invalidProcessResultType;
        }

        // Arrange
        foreach (var hasThrownException in hasThrownExceptionCollection)
            hasThrownException.Should().BeTrue();
    }
}
