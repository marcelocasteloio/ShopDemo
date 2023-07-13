using ShopDemo.SharedKernel.ValueObjects;

namespace ShopDemo.Tests.UnitTests.SharedKernelTests.ValueObjectsTests;
public class TenantInfoValueObjectTests
{
    private const string CONTEXT = "SharedKernel";
    private const string OBJECT_NAME = nameof(TenantInfoValueObject);

    [Fact(DisplayName = "Should generate from existing code")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void TenantInfoValueObject_Should_Generate_From_Existing_Code()
    {
        // Arrange 
        var existingCode = Guid.NewGuid();

        // Act
        var tenantInfo = TenantInfoValueObject.FromExistingCode(existingCode);

        // Assert
        tenantInfo.Code.Should().Be(existingCode);
    }

    [Fact(DisplayName = "Should generate HashCode base on value")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void TenantInfoValueObject_Should_Generate_HashCode_Based_On_Value()
    {
        // Arrange
        var tenantInfoA = TenantInfoValueObject.FromExistingCode(Guid.NewGuid());
        var tenantInfoB = tenantInfoA;
        var tenantInfoC = TenantInfoValueObject.FromExistingCode(Guid.NewGuid());

        // Act
        var hashCodeA = tenantInfoA.GetHashCode();
        var hashCodeB = tenantInfoB.GetHashCode();
        var hashCodeC = tenantInfoC.GetHashCode();

        // Assert
        hashCodeA.Should().Be(hashCodeB);
        hashCodeA.Should().NotBe(hashCodeC);
    }

    [Fact(DisplayName = "Should correctly represented in string")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void TenantInfoValueObject_Should_Correctly_Represented_In_String()
    {
        // Arrange
        var id = Guid.NewGuid();
        var TenantInfoValueObject = (TenantInfoValueObject)id;
        var expected = id.ToString();

        // Act
        var toStringResult = TenantInfoValueObject.ToString();

        // Assert
        toStringResult.Should().Be(expected);
    }

    [Fact(DisplayName = "Should use equals method based on value")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void TenantInfoValueObject_Should_Use_Equals_Method_Based_On_Value()
    {
        // Arrange
        var tenantInfoA = TenantInfoValueObject.FromExistingCode(Guid.NewGuid());
        var tenantInfoB = tenantInfoA;
        var tenantInfoC = TenantInfoValueObject.FromExistingCode(Guid.NewGuid());

        // Act
        var resultA = tenantInfoA.Equals(tenantInfoB);
        var resultB = tenantInfoA.Equals(tenantInfoC);
        var resultC = tenantInfoA.Equals(null);

        // Assert
        resultA.Should().BeTrue();
        resultB.Should().BeFalse();
        resultC.Should().BeFalse();
    }

    [Fact(DisplayName = "Should use equals operator")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void TenantInfoValueObject_Should_Use_Equals_Operator()
    {
        // Arrange
        var tenantInfoA = TenantInfoValueObject.FromExistingCode(Guid.NewGuid());
        var tenantInfoB = tenantInfoA;
        var tenantInfoC = TenantInfoValueObject.FromExistingCode(Guid.NewGuid());

        // Act
        var resultA = tenantInfoA == tenantInfoB;
        var resultB = tenantInfoA == tenantInfoC;

        // Assert
        resultA.Should().BeTrue();
        resultB.Should().BeFalse();
    }

    [Fact(DisplayName = "Should use not equals operator")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void TenantInfoValueObject_Should_Use_NotEquals_Operator()
    {
        // Arrange
        var tenantInfoA = TenantInfoValueObject.FromExistingCode(Guid.NewGuid());
        var tenantInfoB = tenantInfoA;
        var tenantInfoC = TenantInfoValueObject.FromExistingCode(Guid.NewGuid());

        // Act
        var resultA = tenantInfoA != tenantInfoB;
        var resultB = tenantInfoA != tenantInfoC;

        // Assert
        resultA.Should().BeFalse();
        resultB.Should().BeTrue();
    }

    [Fact(DisplayName = "Should implicit converted from Guid")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void TenantInfoValueObject_Should_Implicit_converted_from_guid()
    {
        // Arrange
        var existingCode = Guid.NewGuid();

        // Act
        TenantInfoValueObject tenantInfo = existingCode; 

        // Assert
        tenantInfo.Code.Should().Be(existingCode);
    }

    [Fact(DisplayName = "Should implicit converted to Guid")]
    [Trait(CONTEXT, OBJECT_NAME)]
    public void TenantInfoValueObject_Should_Implicit_converted_to_guid()
    {
        // Arrange
        var existingTenantInfo = TenantInfoValueObject.FromExistingCode(Guid.NewGuid());

        // Act
        Guid code = existingTenantInfo;

        // Assert
        code.Should().Be(existingTenantInfo.Code);
    }
}
