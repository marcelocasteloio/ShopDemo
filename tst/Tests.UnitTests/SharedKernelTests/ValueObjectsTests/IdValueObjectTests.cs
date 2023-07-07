using ShopDemo.SharedKernel.ValueObjects;
using System.Collections.Concurrent;

namespace ShopDemo.Tests.UnitTests.SharedKernelTests.ValueObjectsTests;
public class IdValueObjectTests
{
    private const string CONTEXT = "SharedKernel";
    private const string OBJECT_NAME = nameof(IdValueObject);

    [Fact(DisplayName = "Should generate new id")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void IdValueObject_Should_Generate_New_Id()
    {
        // Arrange and Act
        var id = IdValueObject.GenerateNewId();

        // Assert
        id.Value.Should().NotBe(Guid.Empty);
    }


    [Fact(DisplayName = "Should generate new id in single thread")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void IdValueObject_Should_Generate_New_Id_In_Single_Thread()
    {
        // Arrange
        var listSize = 1_000_000;
        var idList = new List<IdValueObject>(listSize);

        // Act
        for (int i = 0; i < listSize; i++)
        {
            idList.Add(IdValueObject.GenerateNewId());
        }

        // Assert
        for (int i = 0; i < idList.Count - 1; i++)
            (idList[i] < idList[i + 1]).Should().BeTrue();
    }

    [Fact(DisplayName = "Should generate new id in multi thread")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void IdValueObject_Should_Generate_New_Id_In_Multi_Thread()
    {
        // Arrange
        var idCount = 1_000_000;
        var chunkSize = 10_000;
        var dict = new Dictionary<int, List<IdValueObject>>();

        var chunkCollection = Enumerable.Range(1, idCount).Chunk(chunkSize).ToArray();
        for (int i = 0; i < chunkCollection.Length; i++)
            dict.Add(i, new List<IdValueObject>(chunkSize));

        // Act
        Parallel.For(0, chunkCollection.Length, i =>
        {
            var chunk = chunkCollection[i];

            for (int j = 0; j < chunk.Length; j++)
                dict[i].Add(IdValueObject.GenerateNewId());
        });

        // Assert
        foreach (var item in dict)
        {
            var concurrentBag = item.Value.ToList();

            for (int i = 0;i < concurrentBag.Count - 1;i++)
                (concurrentBag[i] < concurrentBag[i + 1]).Should().BeTrue();
        }
    }

    [Fact(DisplayName = "Should generate new sequential id")]
    [Trait(CONTEXT, OBJECT_NAME)]
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
    [Trait(CONTEXT, OBJECT_NAME)]
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
    [Trait(CONTEXT, OBJECT_NAME)]
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
    [Trait(CONTEXT, OBJECT_NAME)]
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
    [Trait(CONTEXT, OBJECT_NAME)]
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
    [Trait(CONTEXT, OBJECT_NAME)]
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

    [Fact(DisplayName = "Should correctly represented in string")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void IdValueObject_Should_Correctly_Represented_In_String()
    {
        // Arrange
        var id = Guid.NewGuid();
        var idValueObject = (IdValueObject)id;
        var expected = id.ToString();

        // Act
        var toStringResult = idValueObject.ToString();

        // Assert
        toStringResult.Should().Be(expected);
    }

    [Fact(DisplayName = "Should use equals method based on value")]
    [Trait(CONTEXT, OBJECT_NAME)]
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
    [Trait(CONTEXT, OBJECT_NAME)]
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
    [Trait(CONTEXT, OBJECT_NAME)]
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
    [Trait(CONTEXT, OBJECT_NAME)]
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
    [Trait(CONTEXT, OBJECT_NAME)]
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
    [Trait(CONTEXT, OBJECT_NAME)]
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
    [Trait(CONTEXT, OBJECT_NAME)]
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
