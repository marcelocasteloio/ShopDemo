using ShopDemo.SharedKernel.Exceptions;
using ShopDemo.SharedKernel.ValueObjects;

namespace ShopDemo.Tests.UnitTests.SharedKernelTests.ValueObjectsTests;
public class DateTimeValueObjectTests
{
    private const string CONTEXT = "SharedKernel";
    private const string OBJECT_NAME = nameof(DateTimeValueObject);

    [Fact(DisplayName = "Should generated from utc now")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public async Task DateTimeValueObject_Should_Generated_From_Utc_Now()
    {
        // Arrange
        var previousDateTime = DateTimeOffset.UtcNow;
        await Task.Delay(TimeSpan.FromMilliseconds(3));

        // Act
        var datetime = DateTimeValueObject.FromUtcNow();

        // Assert
        datetime.Value.Should().BeAfter(previousDateTime);
    }

    [Fact(DisplayName = "Should generated from existing DateTimeValueObject")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public async Task DateTimeValueObject_Should_Generated_From_Existing_DateTimeValueObject()
    {
        // Arrange
        var existingDateTimeValueObject = DateTimeValueObject.FromUtcNow();
        await Task.Delay(TimeSpan.FromMilliseconds(3));

        // Act
        var datetime = DateTimeValueObject.FromExistingDateTimeValueObject(existingDateTimeValueObject);

        // Assert
        datetime.Value.Should().Be(existingDateTimeValueObject.Value);
    }

    [Fact(DisplayName = "Should generated from existing DateTime")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public async Task DateTimeValueObject_Should_Generated_From_Existing_DateTime()
    {
        // Arrange
        var existingDateTime = DateTime.UtcNow;
        await Task.Delay(TimeSpan.FromMilliseconds(3));

        // Act
        var datetime = DateTimeValueObject.FromExistingDateTime(existingDateTime);

        // Assert
        datetime.Value.Should().Be(existingDateTime);
    }

    [Fact(DisplayName = "Should not generated from existing DateTime without utc kind")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void DateTimeValueObject_Should_Not_Generated_From_Existing_DateTime_Without_Utc_Kind()
    {
        // Arrange
        var existingDateTime = DateTime.Now;
        var hasThrownException = false;

        // Act
        try
        {
            _ = DateTimeValueObject.FromExistingDateTime(existingDateTime);
        }
        catch (DateTimeShouldBeUtcException)
        {
            hasThrownException = true;
        }

        // Assert
        hasThrownException.Should().BeTrue();
    }


    [Fact(DisplayName = "Should generated from existing DateTimeOffset")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public async Task DateTimeValueObject_Should_Generated_From_Existing_DateTimeOffset()
    {
        // Arrange
        var existingDateTimeOffset = DateTimeOffset.UtcNow;
        await Task.Delay(TimeSpan.FromMilliseconds(3));

        // Act
        var datetime = DateTimeValueObject.FromExistingDateTimeOffset(existingDateTimeOffset);

        // Assert
        datetime.Value.Should().Be(existingDateTimeOffset);
    }

    [Fact(DisplayName = "Should get HashCode based on value")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public async Task DateTimeValueObject_Should_Get_HashCode_Based_On_Value()
    {
        // Arrange
        var existingDateTimeOffset = DateTimeOffset.UtcNow;
        var existingHashCode = existingDateTimeOffset.GetHashCode();
        await Task.Delay(TimeSpan.FromMilliseconds(3));
        var anotherExistingHashCode = DateTimeValueObject.FromUtcNow().GetHashCode();

        // Act
        var datetime = DateTimeValueObject.FromExistingDateTimeOffset(existingDateTimeOffset);
        var hashCode = datetime.GetHashCode();

        // Assert
        hashCode.Should().Be(existingHashCode);
        hashCode.Should().NotBe(anotherExistingHashCode);
    }

    [Fact(DisplayName = "Should correctly represented in string")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public async Task DateTimeValueObject_Should_Correctly_Represented_In_String()
    {
        // Arrange
        var existingDateTimeOffset = DateTimeOffset.UtcNow;
        var expected = existingDateTimeOffset.ToString();
        await Task.Delay(TimeSpan.FromMilliseconds(3));

        // Act
        var datetime = DateTimeValueObject.FromExistingDateTimeOffset(existingDateTimeOffset);
        var dateTimeToStringResult = datetime.ToString();

        // Assert
        dateTimeToStringResult.Should().Be(expected);
    }

    [Fact(DisplayName = "Should use equals method based on value")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public async Task DateTimeValueObject_Should_Use_Equals_Method_Based_On_Value()
    {
        // Arrange
        var idA = DateTimeValueObject.FromUtcNow();
        var idB = idA;
        await Task.Delay(TimeSpan.FromMilliseconds(3));
        var idC = DateTimeValueObject.FromUtcNow();

        // Act
        var resultA = idA.Equals(idB);
        var resultB = idA.Equals(idC);
        var resultC = idA.Equals(null);

        // Assert
        resultA.Should().BeTrue();
        resultB.Should().BeFalse();
        resultC.Should().BeFalse();
    }

    [Fact(DisplayName = "Should use equals operator")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void DateTimeValueObject_Should_Use_Equals_Operator()
    {
        // Arrange
        var idA = DateTimeValueObject.FromUtcNow();
        var idB = idA;
        var idC = DateTimeValueObject.FromUtcNow();

        // Act
        var resultA = idA == idB;
        var resultB = idA == idC;

        // Assert
        resultA.Should().BeTrue();
        resultB.Should().BeFalse();
    }

    [Fact(DisplayName = "Should use not equals operator")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void DateTimeValueObject_Should_Use_NotEquals_Operator()
    {
        // Arrange
        var idA = DateTimeValueObject.FromUtcNow();
        var idB = idA;
        var idC = DateTimeValueObject.FromUtcNow();

        // Act
        var resultA = idA != idB;
        var resultB = idA != idC;

        // Assert
        resultA.Should().BeFalse();
        resultB.Should().BeTrue();
    }

    [Fact(DisplayName = "Should use greater than operator")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void DateTimeValueObject_Should_Use_Greater_Than_Operator()
    {
        // Arrange
        var idA = DateTimeValueObject.FromUtcNow();
        var idB = idA;
        var idC = DateTimeValueObject.FromUtcNow();

        // Act
        var resultA = idA > idB;
        var resultB = idC > idA;

        // Assert
        resultA.Should().BeFalse();
        resultB.Should().BeTrue();
    }

    [Fact(DisplayName = "Should use greater than or equals operator")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void DateTimeValueObject_Should_Use_Greater_Than_Or_Equals_Operator()
    {
        // Arrange
        var idA = DateTimeValueObject.FromUtcNow();
        var idB = DateTimeValueObject.FromUtcNow();
        var idC = idB;
        var idD = DateTimeValueObject.FromUtcNow();

        // Act
        var resultA = idB >= idA;
        var resultB = idB >= idC;
        var resultC = idA >= idD;

        // Assert
        resultA.Should().BeTrue();
        resultB.Should().BeTrue();
        resultC.Should().BeFalse();
    }

    [Fact(DisplayName = "Should use less than operator")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void DateTimeValueObject_Should_Use_Less_Than_Operator()
    {
        // Arrange
        var idA = DateTimeValueObject.FromUtcNow();
        var idB = idA;
        var idC = DateTimeValueObject.FromUtcNow();

        // Act
        var resultA = idB < idA;
        var resultB = idA < idC;

        // Assert
        resultA.Should().BeFalse();
        resultB.Should().BeTrue();
    }

    [Fact(DisplayName = "Should use less than or equals operator")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void DateTimeValueObject_Should_Use_Less_Than_Or_Equals_Operator()
    {
        // Arrange
        var idA = DateTimeValueObject.FromUtcNow();
        var idB = DateTimeValueObject.FromUtcNow();
        var idC = idB;
        var idD = DateTimeValueObject.FromUtcNow();

        // Act
        var resultA = idA <= idB;
        var resultB = idC <= idB;
        var resultC = idD <= idA;

        // Assert
        resultA.Should().BeTrue();
        resultB.Should().BeTrue();
        resultC.Should().BeFalse();
    }

    [Fact(DisplayName = "Should implicited converted from DateTimeOffset")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void DateTimeValueObject_Should_Implicited_Converted_From_DateTimeOffset()
    {
        // Arrange
        var existingDateTimeOffset = DateTimeOffset.UtcNow;

        // Act
        DateTimeValueObject datetime = existingDateTimeOffset;

        // Arrange
        datetime.Value.Should().Be(existingDateTimeOffset);
    }

    [Fact(DisplayName = "Should implicited converted to DateTimeOffset")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void DateTimeValueObject_Should_Implicited_Converted_To_DateTimeOffset()
    {
        // Arrange
        var existingDateTimeValueObject = DateTimeValueObject.FromUtcNow();

        // Act
        DateTimeOffset datetimeOffset = existingDateTimeValueObject;

        // Arrange
        datetimeOffset.Should().Be(existingDateTimeValueObject.Value);
    }

    [Fact(DisplayName = "Should implicited converted from DateTime")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void DateTimeValueObject_Should_Implicited_Converted_From_DateTime()
    {
        // Arrange
        var existingDateTime = DateTime.UtcNow;

        // Act
        DateTimeValueObject datetime = existingDateTime;

        // Arrange
        datetime.Value.Should().Be(existingDateTime);
    }

    [Fact(DisplayName = "Should implicited converted to DateTime")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void DateTimeValueObject_Should_Implicited_Converted_To_DateTime()
    {
        // Arrange
        var existingDateTimeValueObject = DateTimeValueObject.FromUtcNow();

        // Act
        DateTime datetime = existingDateTimeValueObject;

        // Arrange
        datetime.Should().Be(existingDateTimeValueObject.Value.DateTime);
    }
}
