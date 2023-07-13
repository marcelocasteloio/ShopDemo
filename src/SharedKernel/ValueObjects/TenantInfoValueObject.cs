using System.Diagnostics.CodeAnalysis;

namespace ShopDemo.SharedKernel.ValueObjects;

public readonly struct TenantInfoValueObject
{
    // Properties
    public Guid Code { get; }

    // Constructors
    private TenantInfoValueObject(Guid code)
    {
        Code = code;
    }

    // Operators
    public static implicit operator TenantInfoValueObject(Guid value) => new(value);
    public static implicit operator Guid(TenantInfoValueObject value) => value.Code;

    public static bool operator ==(TenantInfoValueObject left, TenantInfoValueObject right) => left.Code == right.Code;
    public static bool operator !=(TenantInfoValueObject left, TenantInfoValueObject right) => left.Code != right.Code;

    // Public Methods
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is not null && this == (TenantInfoValueObject)obj;
    public override int GetHashCode() => Code.GetHashCode();
    public override string ToString() => Code.ToString();

    // Builders
    public static TenantInfoValueObject FromExistingCode(Guid code) => new(code);
}
