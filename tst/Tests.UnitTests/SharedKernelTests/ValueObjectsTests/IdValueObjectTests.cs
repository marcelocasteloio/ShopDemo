using FluentAssertions;
using ShopDemo.SharedKernel.ValueObjects;

namespace ShopDemo.Tests.UnitTests.SharedKernelTests.ValueObjectsTests;
public class IdValueObjectTests
{
    [Fact(DisplayName = "Should generate new id")]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Generate_New_Id()
    {
        // Arrange and Act
        var id = IdValueObject.GenerateNewId();

        // Assert
        id.Value.Should().NotBe(default(Guid));
    }

    [Fact(DisplayName = "Should generate new sequential id")]
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

    [Fact(DisplayName = "Should generated from existing id")]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Generated_From_Existing_Id()
    {
        // Arrange
        var existringId = Guid.NewGuid();

        // Act
        var id = IdValueObject.FromExistingId(existringId);

        // Assert
        id.Value.Should().Be(existringId);
    }

    [Fact(DisplayName = "Should generated from existing IdValueObject")]
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

    [Fact(DisplayName = "Should implicit converted from guid")]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Implicit_Converted_From_Guid()
    {
        // Arrange
        var existringId = Guid.NewGuid();

        // Act
        IdValueObject id = existringId;

        // Assert
        id.Value.Should().Be(existringId);
    }

    [Fact(DisplayName = "Should implicit converted to guid")]
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

    [Fact(DisplayName = "Should generate HashCode base on value")]
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

    [Fact(DisplayName = "Should use equals method based on value")]
    [Trait("SharedKernel", nameof(IdValueObject))]
    public void IdValueObject_Should_Use_Equals_Method_Based_On_Value()
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

    [Fact(DisplayName = "Should use equals operator")]
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

    [Fact(DisplayName = "Should use not equals operator")]
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

    [Fact(DisplayName = "Should use greater than operator")]
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

    [Fact(DisplayName = "Should use greater than or equals operator")]
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

    [Fact(DisplayName = "Should use less than operator")]
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

    [Fact(DisplayName = "Should use less than or equals operator")]
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
