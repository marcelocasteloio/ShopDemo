using System.Runtime.Serialization;

namespace ShopDemo.SharedKernel.Exceptions;

[Serializable]
public class DateTimeShouldBeUtcException
    : Exception
{
    /*
     * This constructors implement sonar rule id csharpsquid:S3925
     * The objective of this rule is make serializations constructors 
     * explicit for programmer not forget of programming the 
     * serialization process if necessary
     */

    // Constructors
    public DateTimeShouldBeUtcException() { }
    public DateTimeShouldBeUtcException(string message) : base(message) { }
    public DateTimeShouldBeUtcException(string message, Exception innerException) : base(message, innerException) { }
    protected DateTimeShouldBeUtcException(SerializationInfo info, StreamingContext context) : base(info, context) { }
}
