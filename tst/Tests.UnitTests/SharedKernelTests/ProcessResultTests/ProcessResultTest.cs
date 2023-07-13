using Microsoft.VisualStudio.TestPlatform.Utilities;
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
            processResult.MessageCollection!.Any(q => q.Code.Equals(successMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(informationMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(warningMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(errorMessageCode)).Should().BeTrue();
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
            processResult.MessageCollection!.Any(q => q.Code.Equals(successMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(informationMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(warningMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(errorMessageCode)).Should().BeTrue();
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
            processResult.MessageCollection!.Any(q => q.Code.Equals(successMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(informationMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(warningMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(errorMessageCode)).Should().BeTrue();
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
            processResult.MessageCollection!.Any(q => q.Code.Equals(successMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(informationMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(warningMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(errorMessageCode)).Should().BeTrue();
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
            processResult.MessageCollection!.Any(q => q.Code.Equals(successMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(informationMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(warningMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(errorMessageCode)).Should().BeTrue();
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
            processResult.MessageCollection!.Any(q => q.Code.Equals(successMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(informationMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(warningMessageCode)).Should().BeTrue();
            processResult.MessageCollection!.Any(q => q.Code.Equals(errorMessageCode)).Should().BeTrue();
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
        }
        .Where(q => q.Type == MessageType.Information || q.Type == MessageType.Success)!
        .ToArray();

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
        }
        .Where(q => q.Type != MessageType.Success)
        .ToArray();

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
        }
        .Where(q => q.Type != MessageType.Success)
        .ToArray();

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

    [Fact(DisplayName = "Should implited converted to bool")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Implicited_Converted_To_Bool()
    {
        // Arrange
        var successProcessResultA = ProcessResult.CreateSuccess();
        var successProcessResultB = ProcessResult<object>.CreateSuccess(output: default);
        var partialProcessResultA = ProcessResult.CreatePartial();
        var partialProcessResultB = ProcessResult<object>.CreatePartial(output: default);
        var errorProcessResultA = ProcessResult.CreateError();
        var errorProcessResultB = ProcessResult<object>.CreateError(output: default);

        // Act
        bool successProcessResultAResult = successProcessResultA;
        bool successProcessResultBResult = successProcessResultB;
        bool partialProcessResultAResult = partialProcessResultA;
        bool partialProcessResultBResult = partialProcessResultB;
        bool errorProcessResultAResult = errorProcessResultA;
        bool errorProcessResultBResult = errorProcessResultB;

        // Assert
        successProcessResultAResult.Should().BeTrue();
        successProcessResultBResult.Should().BeTrue();
        partialProcessResultAResult.Should().BeFalse();
        partialProcessResultBResult.Should().BeFalse();
        errorProcessResultAResult.Should().BeFalse();
        errorProcessResultBResult.Should().BeFalse();
    }

    [Fact(DisplayName = "Should implited converted from bool")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Implicited_Converted_From_Bool()
    {
        // Arrange
        var trueValue = true;
        var falseValue = false;

        // Act
        ProcessResult processResultA = trueValue;
        ProcessResult<object> processResultB = trueValue;
        ProcessResult processResultC = falseValue;
        ProcessResult<object> processResultD = falseValue;

        // Assert
        processResultA.IsSuccess.Should().BeTrue();
        processResultB.IsSuccess.Should().BeTrue();
        processResultC.IsSuccess.Should().BeFalse();
        processResultD.IsSuccess.Should().BeFalse();
    }

    [Fact(DisplayName = "Should create from another single ProcessResult")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Create_From_Another_Single_ProcessResult()
    {
        // Arrange
        var informationMessage = Message.CreateInformation(code: "InfoMessageCode");
        var successMessage = Message.CreateSuccess(code: "SuccessMessageCode");
        var warningMessage = Message.CreateWarning(code: "WarningMessageCode");
        var errorMessage = Message.CreateError(code: "ErrorMessageCode");

        var informationProcessResultA = ProcessResult.FromMessageCollection(new[] { informationMessage });

        var successProcessResultA = ProcessResult.FromMessageCollection(new[] { successMessage });
        var successProcessResultB = ProcessResult.FromMessageCollection(new[] { successMessage, informationMessage });
        
        var partialProcessResultA = ProcessResult.FromMessageCollection(new[] { warningMessage });
        var partialProcessResultB = ProcessResult.FromMessageCollection(new[] { warningMessage, informationMessage });
        var partialProcessResultC = ProcessResult.FromMessageCollection(new[] { warningMessage, successMessage, informationMessage });

        var errorProcessResultA = ProcessResult.FromMessageCollection(new[] { errorMessage });
        var errorProcessResultB = ProcessResult.FromMessageCollection(new[] { errorMessage, warningMessage });
        var errorProcessResultC = ProcessResult.FromMessageCollection(new[] { errorMessage, warningMessage, successMessage });
        var errorProcessResultD = ProcessResult.FromMessageCollection(new[] { errorMessage, warningMessage, successMessage, informationMessage });

        // Act
        var newSuccessProcessResultA = ProcessResult.FromProcessResult(successProcessResultA);
        var newSuccessProcessResultB = ProcessResult.FromProcessResult(successProcessResultB);
        var newSuccessProcessResultC = ProcessResult.FromProcessResult(informationProcessResultA);

        var newWarningProcessResultA = ProcessResult.FromProcessResult(partialProcessResultA);
        var newWarningProcessResultB = ProcessResult.FromProcessResult(partialProcessResultB);
        var newWarningProcessResultC = ProcessResult.FromProcessResult(partialProcessResultC);

        var newErrorProcessResultA = ProcessResult.FromProcessResult(errorProcessResultA);
        var newErrorProcessResultB = ProcessResult.FromProcessResult(errorProcessResultB);
        var newErrorProcessResultC = ProcessResult.FromProcessResult(errorProcessResultC);
        var newErrorProcessResultD = ProcessResult.FromProcessResult(errorProcessResultD);

        // Assert
        var validateAction = new Action<ProcessResult, ProcessResult>((value, expected) =>
        {
            value.Type.Should().Be(expected.Type);
            value.MessageCollection.Should().BeSameAs(expected.MessageCollection);
        });

        validateAction(newSuccessProcessResultA, successProcessResultA);
        validateAction(newSuccessProcessResultB, successProcessResultB);
        validateAction(newSuccessProcessResultC, informationProcessResultA);

        validateAction(newWarningProcessResultA, partialProcessResultA);
        validateAction(newWarningProcessResultB, partialProcessResultB);
        validateAction(newWarningProcessResultC, partialProcessResultC);

        validateAction(newErrorProcessResultA, errorProcessResultA);
        validateAction(newErrorProcessResultB, errorProcessResultB);
        validateAction(newErrorProcessResultC, errorProcessResultC);
        validateAction(newErrorProcessResultD, errorProcessResultD);
    }

    [Fact(DisplayName = "Should create success from another multi ProcessResult")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Create_Success_From_Another_Multi_ProcessResult()
    {
        // Arrange
        var informationMessage = Message.CreateInformation(code: "InfoMessageCode");
        var successMessage = Message.CreateSuccess(code: "SuccessMessageCode");
        var warningMessage = Message.CreateWarning(code: "WarningMessageCode");
        var errorMessage = Message.CreateError(code: "ErrorMessageCode");

        var informationProcessResultA = ProcessResult.FromMessageCollection(new[] { informationMessage });

        var successProcessResultA = ProcessResult.FromMessageCollection(new[] { successMessage });
        var successProcessResultB = ProcessResult.FromMessageCollection(new[] { successMessage, informationMessage });
        var successProcessResultC = ProcessResult.FromMessageCollection(new[] { warningMessage, successMessage, informationMessage });

        var errorProcessResultA = ProcessResult.FromMessageCollection(new[] { errorMessage });
        var errorProcessResultB = ProcessResult.FromMessageCollection(new[] { errorMessage, warningMessage });
        var errorProcessResultC = ProcessResult.FromMessageCollection(new[] { errorMessage, warningMessage, successMessage });
        var errorProcessResultD = ProcessResult.FromMessageCollection(new[] { errorMessage, warningMessage, successMessage, informationMessage });

        var partialProcessResultA = ProcessResult.FromMessageCollection(new[] { errorMessage, warningMessage, successMessage, informationMessage });

        // Act
        var newSuccessProcessResult = ProcessResult.FromProcessResult(
            successProcessResultA, 
            successProcessResultB,
            successProcessResultC,
            informationProcessResultA
        );
        var newErrorProcessResult = ProcessResult.FromProcessResult(
            newSuccessProcessResult,
            errorProcessResultA,
            errorProcessResultB,
            errorProcessResultC,
            errorProcessResultD
        );
        var newPartialProcessResult = ProcessResult.FromProcessResult(
            newSuccessProcessResult,
            partialProcessResultA
        );

        // Assert
        var validateAction = new Action<ProcessResult, ProcessResultType, ProcessResult[]>(
            (value, expectedProcessResultType, expectedProcessResultCollection) =>
            {
                value.Type.Should().Be(expectedProcessResultType);

                var expectedMessageArray = expectedProcessResultCollection.SelectMany(q => q.MessageCollection ?? Array.Empty<Message>()).ToArray();

                value.MessageCollection.Should().HaveCount(expectedMessageArray.Length);

                foreach (var item in expectedProcessResultCollection)
                    item.MessageCollection.Should().BeSubsetOf(expectedMessageArray);
            }
        );

        validateAction(
            newSuccessProcessResult, 
            ProcessResultType.Success, 
            new[] {
                successProcessResultA,
                successProcessResultB,
                successProcessResultC,
                informationProcessResultA
            }
        );
        validateAction(
            newErrorProcessResult,
            ProcessResultType.Error,
            new[] {
                newSuccessProcessResult,
                errorProcessResultA,
                errorProcessResultB,
                errorProcessResultC,
                errorProcessResultD
            }
        );
        validateAction(
            newPartialProcessResult,
            ProcessResultType.Partial,
            new[] {
                newSuccessProcessResult,
                partialProcessResultA
            }
        );
    }

    [Fact(DisplayName = "Should create from another single ProcessResult with output")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Create_From_Another_Single_ProcessResult_With_Output()
    {
        // Arrange
        var output = Guid.NewGuid();
        var informationMessage = Message.CreateInformation(code: "InfoMessageCode");
        var successMessage = Message.CreateSuccess(code: "SuccessMessageCode");
        var warningMessage = Message.CreateWarning(code: "WarningMessageCode");
        var errorMessage = Message.CreateError(code: "ErrorMessageCode");

        var informationProcessResultA = ProcessResult<Guid>.FromMessageCollection(output, new[] { informationMessage });

        var successProcessResultA = ProcessResult<Guid>.FromMessageCollection(output, new[] { successMessage });
        var successProcessResultB = ProcessResult<Guid>.FromMessageCollection(output, new[] { successMessage, informationMessage });

        var partialProcessResultA = ProcessResult<Guid>.FromMessageCollection(output, new[] { warningMessage });
        var partialProcessResultB = ProcessResult<Guid>.FromMessageCollection(output, new[] { warningMessage, informationMessage });
        var partialProcessResultC = ProcessResult<Guid>.FromMessageCollection(output, new[] { warningMessage, successMessage, informationMessage });

        var errorProcessResultA = ProcessResult<Guid>.FromMessageCollection(output, new[] { errorMessage });
        var errorProcessResultB = ProcessResult<Guid>.FromMessageCollection(output, new[] { errorMessage, warningMessage });
        var errorProcessResultC = ProcessResult<Guid>.FromMessageCollection(output, new[] { errorMessage, warningMessage, successMessage });
        var errorProcessResultD = ProcessResult<Guid>.FromMessageCollection(output, new[] { errorMessage, warningMessage, successMessage, informationMessage });

        // Act
        var newSuccessProcessResultA = ProcessResult<Guid>.FromProcessResult(output, successProcessResultA);
        var newSuccessProcessResultB = ProcessResult<Guid>.FromProcessResult(output, successProcessResultB);
        var newSuccessProcessResultC = ProcessResult<Guid>.FromProcessResult(output, informationProcessResultA);

        var newWarningProcessResultA = ProcessResult<Guid>.FromProcessResult(output, partialProcessResultA);
        var newWarningProcessResultB = ProcessResult<Guid>.FromProcessResult(output, partialProcessResultB);
        var newWarningProcessResultC = ProcessResult<Guid>.FromProcessResult(output, partialProcessResultC);

        var newErrorProcessResultA = ProcessResult<Guid>.FromProcessResult(output, errorProcessResultA);
        var newErrorProcessResultB = ProcessResult<Guid>.FromProcessResult(output, errorProcessResultB);
        var newErrorProcessResultC = ProcessResult<Guid>.FromProcessResult(output, errorProcessResultC);
        var newErrorProcessResultD = ProcessResult<Guid>.FromProcessResult(output, errorProcessResultD);

        // Assert
        var validateAction = new Action<ProcessResult<Guid>, ProcessResult<Guid>>((value, expected) =>
        {
            value.Output.Should().Be(output);
            value.Type.Should().Be(expected.Type);
            value.MessageCollection.Should().BeSameAs(expected.MessageCollection);
        });

        validateAction(newSuccessProcessResultA, successProcessResultA);
        validateAction(newSuccessProcessResultB, successProcessResultB);
        validateAction(newSuccessProcessResultC, informationProcessResultA);

        validateAction(newWarningProcessResultA, partialProcessResultA);
        validateAction(newWarningProcessResultB, partialProcessResultB);
        validateAction(newWarningProcessResultC, partialProcessResultC);

        validateAction(newErrorProcessResultA, errorProcessResultA);
        validateAction(newErrorProcessResultB, errorProcessResultB);
        validateAction(newErrorProcessResultC, errorProcessResultC);
        validateAction(newErrorProcessResultD, errorProcessResultD);
    }

    [Fact(DisplayName = "Should create success from another multi ProcessResult with output")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void ProcessResult_Should_Create_Success_From_Another_Multi_ProcessResult_With_Output()
    {
        // Arrange
        var output = Guid.NewGuid();
        var informationMessage = Message.CreateInformation(code: "InfoMessageCode");
        var successMessage = Message.CreateSuccess(code: "SuccessMessageCode");
        var warningMessage = Message.CreateWarning(code: "WarningMessageCode");
        var errorMessage = Message.CreateError(code: "ErrorMessageCode");

        var informationProcessResultA = ProcessResult<Guid>.FromMessageCollection(output, new[] { informationMessage });

        var successProcessResultA = ProcessResult<Guid>.FromMessageCollection(output, new[] { successMessage });
        var successProcessResultB = ProcessResult<Guid>.FromMessageCollection(output, new[] { successMessage, informationMessage });
        var successProcessResultC = ProcessResult<Guid>.FromMessageCollection(output, new[] { warningMessage, successMessage, informationMessage });

        var errorProcessResultA = ProcessResult<Guid>.FromMessageCollection(output, new[] { errorMessage });
        var errorProcessResultB = ProcessResult<Guid>.FromMessageCollection(output, new[] { errorMessage, warningMessage });
        var errorProcessResultC = ProcessResult<Guid>.FromMessageCollection(output, new[] { errorMessage, warningMessage, successMessage });
        var errorProcessResultD = ProcessResult<Guid>.FromMessageCollection(output, new[] { errorMessage, warningMessage, successMessage, informationMessage });

        var partialProcessResultA = ProcessResult<Guid>.FromMessageCollection(output, new[] { errorMessage, warningMessage, successMessage, informationMessage });

        // Act
        var newSuccessProcessResult = ProcessResult<Guid>.FromProcessResult(
            output,
            successProcessResultA,
            successProcessResultB,
            successProcessResultC,
            informationProcessResultA
        );
        var newErrorProcessResult = ProcessResult<Guid>.FromProcessResult(
            output,
            newSuccessProcessResult,
            errorProcessResultA,
            errorProcessResultB,
            errorProcessResultC,
            errorProcessResultD
        );
        var newPartialProcessResult = ProcessResult<Guid>.FromProcessResult(
            output,
            newSuccessProcessResult,
            partialProcessResultA
        );

        // Assert
        var validateAction = new Action<ProcessResult<Guid>, ProcessResultType, ProcessResult<Guid>[]>(
            (value, expectedProcessResultType, expectedProcessResultCollection) =>
            {
                value.Output.Should().Be(output);
                value.Type.Should().Be(expectedProcessResultType);

                var expectedMessageArray = expectedProcessResultCollection.SelectMany(q => q.MessageCollection ?? Array.Empty<Message>()).ToArray();

                value.MessageCollection.Should().HaveCount(expectedMessageArray.Length);

                foreach (var item in expectedProcessResultCollection)
                    item.MessageCollection.Should().BeSubsetOf(expectedMessageArray);
            }
        );

        validateAction(
            newSuccessProcessResult,
            ProcessResultType.Success,
            new[] {
                successProcessResultA,
                successProcessResultB,
                successProcessResultC,
                informationProcessResultA
            }
        );
        validateAction(
            newErrorProcessResult,
            ProcessResultType.Error,
            new[] {
                newSuccessProcessResult,
                errorProcessResultA,
                errorProcessResultB,
                errorProcessResultC,
                errorProcessResultD
            }
        );
        validateAction(
            newPartialProcessResult,
            ProcessResultType.Partial,
            new[] {
                newSuccessProcessResult,
                partialProcessResultA
            }
        );
    }
}
