using MassTransit.NewIdProviders;
using MassTransit;
using System.Diagnostics.CodeAnalysis;

namespace ShopDemo.SharedKernel.ValueObjects;
public readonly struct IdValueObject
{
    // Properties
    public Guid Value { get; }

    // Constructors
    static IdValueObject()
    {
        /*
         * Add current process id to guid generation process
         * to avoid problemns with multithread id generations
         */
        NewId.SetProcessIdProvider(new CurrentProcessIdProvider());
    }
    private IdValueObject(Guid value)
    {
        Value = value;
    }
    private IdValueObject(IdValueObject value)
    {
        Value = value.Value;
    }

    // Operators
    public static implicit operator IdValueObject(Guid value) => new(value);
    public static implicit operator Guid(IdValueObject value) => value.Value;

    public static bool operator ==(IdValueObject left, IdValueObject right) => left.Value == right.Value;
    public static bool operator !=(IdValueObject left, IdValueObject right) => !(left.Value == right.Value);
    public static bool operator >(IdValueObject left, IdValueObject right) =>  left.Value > right.Value;
    public static bool operator >=(IdValueObject left, IdValueObject right) =>  left.Value >= right.Value;
    public static bool operator <(IdValueObject left, IdValueObject right) =>  left.Value < right.Value;
    public static bool operator <=(IdValueObject left, IdValueObject right) =>  left.Value <= right.Value;

    // Public Methods
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is not null && this == (IdValueObject)obj;
    public override int GetHashCode() => Value.GetHashCode();

    // Builders
    public static IdValueObject GenerateNewId() => NewId.NextGuid();
    public static IdValueObject FromExistingId(Guid id) => new IdValueObject(id).Value;
    public static IdValueObject FromExistingId(IdValueObject id) => new IdValueObject(id).Value;
}
