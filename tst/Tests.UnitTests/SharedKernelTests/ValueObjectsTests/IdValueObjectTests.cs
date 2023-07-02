using FluentAssertions;
using ShopDemo.SharedKernel.ValueObjects;
using System.ComponentModel;

namespace ShopDemo.Tests.UnitTests.SharedKernelTests.ValueObjectsTests;
public class IdValueObjectTests
{
    [Fact]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Generate_New_Id()
    {
        // Arrange and Act
        var id = IdValueObject.GenerateNewId();

        // Assert
        id.Value.Should().NotBe(default(Guid));
    }

    [Fact]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Generate_New_Sequential_Id()
    {
        // Arrange
        var previousId = IdValueObject.GenerateNewId();

        // Act
        var nextId = IdValueObject.GenerateNewId();

        // Assert
        (nextId > previousId).Should().BeTrue();
    }

    [Fact]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Generate_From_Existing_Id()
    {
        // Arrange
        var existringId = Guid.NewGuid();

        // Act
        var id = IdValueObject.FromExistingId(existringId);

        // Assert
        id.Value.Should().Be(existringId);
    }

    [Fact]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Generate_From_Existing_IdValueObject()
    {
        // Arrange
        var existringId = IdValueObject.GenerateNewId();

        // Act
        var id = IdValueObject.FromExistingId(existringId);

        // Assert
        id.Value.Should().Be(existringId);
    }

    [Fact]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Implict_Converted_From_Guid()
    {
        // Arrange
        var existringId = Guid.NewGuid();

        // Act
        IdValueObject id = existringId;

        // Assert
        id.Value.Should().Be(existringId);
    }

    [Fact]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Implict_Converted_To_Guid()
    {
        // Arrange
        var existringId = IdValueObject.GenerateNewId();

        // Act
        Guid id = existringId;

        // Assert
        id.Should().Be(existringId.Value);
    }

    [Fact]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Generate_HashCode_Based_On_Value()
    {
        // Arrange
        var idA = IdValueObject.GenerateNewId();
        var idB = idA;
        var idC = IdValueObject.GenerateNewId();

        // Act
        var hashCodeA = idA.GetHashCode();
        var hashCodeB = idB.GetHashCode();
        var hashCodeC = idC.GetHashCode();

        // Assert
        hashCodeA.Should().Be(hashCodeB);
        hashCodeA.Should().NotBe(hashCodeC);
    }

    [Fact]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Use_Equals_Method()
    {
        // Arrange
        var idA = IdValueObject.GenerateNewId();
        var idB = idA;
        var idC = IdValueObject.GenerateNewId();

        // Act
        var resultA = idA.Equals(idB);
        var resultB = idA.Equals(idC);
        var resultC = idA.Equals(null);

        // Assert
        resultA.Should().BeTrue();
        resultB.Should().BeFalse();
        resultC.Should().BeFalse();
    }

    [Fact]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Use_Equals_Operator()
    {
        // Arrange
        var idA = IdValueObject.GenerateNewId();
        var idB = idA;
        var idC = IdValueObject.GenerateNewId();

        // Act
        var resultA = idA == idB;
        var resultB = idA == idC;

        // Assert
        resultA.Should().BeTrue();
        resultB.Should().BeFalse();
    }

    [Fact]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Use_NotEquals_Operator()
    {
        // Arrange
        var idA = IdValueObject.GenerateNewId();
        var idB = idA;
        var idC = IdValueObject.GenerateNewId();

        // Act
        var resultA = idA != idB;
        var resultB = idA != idC;

        // Assert
        resultA.Should().BeFalse();
        resultB.Should().BeTrue();
    }

    [Fact]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Use_Greater_Than_Operator()
    {
        // Arrange
        var idA = IdValueObject.GenerateNewId();
        var idB = idA;
        var idC = IdValueObject.GenerateNewId();

        // Act
        var resultA = idA > idB;
        var resultB = idC > idA;

        // Assert
        resultA.Should().BeFalse();
        resultB.Should().BeTrue();
    }

    [Fact]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Use_Greater_Than_Or_Equals_Operator()
    {
        // Arrange
        var idA = IdValueObject.GenerateNewId();
        var idB = IdValueObject.GenerateNewId();
        var idC = idB;
        var idD = IdValueObject.GenerateNewId();

        // Act
        var resultA = idB >= idA;
        var resultB = idB >= idC;
        var resultC = idA >= idD;

        // Assert
        resultA.Should().BeTrue();
        resultB.Should().BeTrue();
        resultC.Should().BeFalse();
    }

    [Fact]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Use_Less_Than_Operator()
    {
        // Arrange
        var idA = IdValueObject.GenerateNewId();
        var idB = idA;
        var idC = IdValueObject.GenerateNewId();

        // Act
        var resultA = idB < idA;
        var resultB = idA < idC;

        // Assert
        resultA.Should().BeFalse();
        resultB.Should().BeTrue();
    }

    [Fact]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Use_Less_Than_Or_Equals_Operator()
    {
        // Arrange
        var idA = IdValueObject.GenerateNewId();
        var idB = IdValueObject.GenerateNewId();
        var idC = idB;
        var idD = IdValueObject.GenerateNewId();

        // Act
        var resultA = idA <= idB;
        var resultB = idC <= idB;
        var resultC = idD <= idA;

        // Assert
        resultA.Should().BeTrue();
        resultB.Should().BeTrue();
        resultC.Should().BeFalse();
    }
}
