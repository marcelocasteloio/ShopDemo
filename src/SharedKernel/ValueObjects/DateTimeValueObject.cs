using ShopDemo.SharedKernel.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace ShopDemo.SharedKernel.ValueObjects;
public readonly struct DateTimeValueObject
{
    // Properties
    public DateTimeOffset Value { get; }

    // Constructors
    private DateTimeValueObject(DateTimeOffset value)
    {
        Value = value;
    }
    private DateTimeValueObject(DateTime value)
    {
        if (value.Kind != DateTimeKind.Utc)
            throw new DateTimeShouldBeUtcException();

        Value = new DateTimeOffset(dateTime: value, offset: TimeSpan.Zero);
    }

    // Operators
    public static implicit operator DateTimeValueObject(DateTimeOffset value) => new(value);
    public static implicit operator DateTimeOffset(DateTimeValueObject value) => value.Value;
    public static implicit operator DateTimeValueObject(DateTime value) => new(value);
    public static implicit operator DateTime(DateTimeValueObject value) => value.Value.DateTime;

    public static bool operator ==(DateTimeValueObject left, DateTimeValueObject right) => left.Value == right.Value;
    public static bool operator !=(DateTimeValueObject left, DateTimeValueObject right) => !(left.Value == right.Value);
    public static bool operator >(DateTimeValueObject left, DateTimeValueObject right) => left.Value > right.Value;
    public static bool operator >=(DateTimeValueObject left, DateTimeValueObject right) => left.Value >= right.Value;
    public static bool operator <(DateTimeValueObject left, DateTimeValueObject right) => left.Value < right.Value;
    public static bool operator <=(DateTimeValueObject left, DateTimeValueObject right) => left.Value <= right.Value;

    // Public Methods
    public override bool Equals([NotNullWhen(true)] object? obj) => obj is not null && this == (DateTimeValueObject)obj;
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value.ToString();

    // Builders
    public static DateTimeValueObject FromUtcNow() => new(DateTimeOffset.UtcNow);
    public static DateTimeValueObject FromExistingDateTimeValueObject(DateTimeValueObject value) => new(value.Value);
    public static DateTimeValueObject FromExistingDateTime(DateTime value) => new(value);
    public static DateTimeValueObject FromExistingDateTimeOffset(DateTimeOffset value) => new(value);
}
